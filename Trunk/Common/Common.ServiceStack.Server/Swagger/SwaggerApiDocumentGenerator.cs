using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;


using ServiceStack.ServiceInterface.ServiceModel;
using SportsWebPt.Common.Utilities.ServiceApi;

namespace SportsWebPt.Common.ServiceStack
{
    public class SwaggerApiDocumentGenerator : ApiDocumentGenerator
    {

        #region Construction

        public SwaggerApiDocumentGenerator(IEnumerable<String> assemblyNames)
            : base(assemblyNames)
        {;}

        #endregion

        #region Methods

        protected override string BuildApiResourceDescription(Type metaType)
        {
            var restAttr = metaType.GetCustomAttributes(typeof(ApiResourceAttribute), true);
            if (restAttr.Length == 0)
                return String.Empty;

            var apiAttribute = restAttr[0] as ApiResourceAttribute;
            if (apiAttribute == null)
                return String.Empty;

            var hashtable = new Hashtable
                                {
                                    {"path", apiAttribute.Path},
                                    {"description", apiAttribute.Description},
                                    {"name", apiAttribute.Name}
                                };

            ApiResourceListing.Add(apiAttribute.Name, hashtable);

            return apiAttribute.Name;
        }

        protected override void BuildApiResourceDeclaration(String resourceName, Type serviceType)
        {
            Dictionary<string, Dictionary<string, object>> operationListings;
            Dictionary<string, Dictionary<string, object>> modelListings;

            if (!ApiOperationListing.TryGetValue(resourceName, out operationListings))
                operationListings = new Dictionary<string, Dictionary<string, object>>();

            var requestResponseTypes = serviceType.BaseType == null ? new Type[] {} : serviceType.BaseType.GetGenericArguments();

            //TODO: not sure if I like using the following types to find appropriate types
            var requestType =
                requestResponseTypes.SingleOrDefault(p => typeof(AbstractResourceRequest).IsAssignableFrom(p));

            var responseType =
                requestResponseTypes.SingleOrDefault(p => typeof(IHasResponseStatus).IsAssignableFrom(p));

            BuildOps(requestType,responseType,operationListings);

            if (!ApiModelListing.TryGetValue(resourceName, out modelListings))
                modelListings = GetModel(new[] { requestType, responseType});

            ApiOperationListing[resourceName] = operationListings;
            ApiModelListing[resourceName] = modelListings;

        }

        private void BuildOps(Type requestType, Type responseType, Dictionary<string, Dictionary<string, object>> operationListings)
        {
            var operationAttributes = requestType.GetCustomAttributes(typeof (ApiOperationAttribute), true);


            foreach (var operationAttribute in operationAttributes.OfType<ApiOperationAttribute>())
            {
                Dictionary<string, object> apiEndpoint;
                if (!operationListings.TryGetValue(operationAttribute.Path, out apiEndpoint))
                {
                    apiEndpoint = BuildApiEndpoint(operationAttribute);
                    operationListings.Add(operationAttribute.Path, apiEndpoint);
                }

                var operations = apiEndpoint["operations"] as List<Dictionary<string, object>>;

                if (operations == null)
                    continue;

                var operation = new Dictionary<string, object>
                                    {
                                        {"httpMethod", operationAttribute.HttpMethod},
                                        {"nickname", operationAttribute.Nickname},
                                        {"responseClass", responseType.Name},
                                        {"parameters", GetParameters(requestType,operationAttribute)},
                                        {"summary", operationAttribute.Summary}
                                    };
                operations.Add(operation);


                //notes: "Only Pets which you have permission to see will be returned",
                //errorResponses:[ ... ]
            }
        }

        private Dictionary<string, object> BuildApiEndpoint(ApiOperationAttribute operationAttribute)
        {
            var apiEndpoint = new Dictionary<string, object>
                                  {
                                      {"path", operationAttribute.Path},
                                      {"description", operationAttribute.Summary},
                                      {"operations", new List<Dictionary<string,object>>() }
                                  };

            return apiEndpoint;
        }

        #endregion

        #region Helpers

        static string GetDesc(PropertyInfo prop)
        {
            var desc = prop.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (desc.Length > 0)
            {
                return string.Join("\n", from d in desc select ((DescriptionAttribute)d).Description);
            }
            return string.Empty;
        }
        static string GetDesc(Type prop)
        {
            var desc = prop.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (desc.Length > 0)
            {
                return string.Join("\n", from d in desc select ((DescriptionAttribute)d).Description);
            }
            return string.Empty;
        }

        private static List<Dictionary<string, object>> GetParameters(Type requestType, ApiOperationAttribute operationAttribute)
        {
            var parameters = new List<Dictionary<string, object>>();
            foreach (var prop in requestType.GetProperties())
            {
                if (prop.CanWrite)
                {
                    var p = new Dictionary<string, object>
                    {
                        { "name", prop.Name },
                        { "description", GetDesc(prop) }
                    };

                    MapDatatype(p, prop.PropertyType);

                    if (operationAttribute.Path.Contains("{" + prop.Name + "}"))
                        p["paramType"] = "path";
                    else
                        p["paramType"] = "query";
                    parameters.Add(p);
                }
            }

            return parameters;
        }

        static readonly Dictionary<Type, string> _typeMap = new Dictionary<Type, string>
		{
			{ typeof(int), "integer" },
			{ typeof(short), "integer" },
			{ typeof(long), "long" },
			{ typeof(double), "double" },
			{ typeof(float), "double" },
			{ typeof(string), "string" },
			{ typeof(bool), "boolean" },
			{ typeof(DateTime), "string" },
			{ typeof(Uri), "string" }

		};

        private static void MapDatatype(Dictionary<string, object> p, Type t)
        {
            try
            {   
                var underlying = Nullable.GetUnderlyingType(t);
                if (underlying != null)
                    t = underlying;

                var dataMemberAttr = t.GetCustomAttributes(typeof (DataMemberAttribute), false).FirstOrDefault() as DataMemberAttribute;

                p["required"] = false;
                if (dataMemberAttr != null && dataMemberAttr.IsRequired)
                    p["required"] = true;

                if (_typeMap.ContainsKey(t))
                    p["dataType"] = _typeMap[t];
                else if (typeof(IEnumerable).IsAssignableFrom(t))
                    p["dataType"] = "array";
                else if (t.IsEnum)
                {
                    p["dataType"] = "string";
                    p["allowableValues"] =
                        new
                        {
                            valueType = "LIST",
                            values = (from object v in Enum.GetValues(t) select v.ToString()).ToArray()
                        };
                }
                else
                    p["dataType"] = t.Name;
            }
            catch
            {
                throw new ArgumentException(string.Format("Cannot map type '{0}' to Swagger type", t));
            }
        }

        private static void MapModelType(Dictionary<string, object> p, Type t)
        {
            try
            {
                var underlying = Nullable.GetUnderlyingType(t);
                if (underlying != null)
                    t = underlying;

                if (_typeMap.ContainsKey(t))
                    p["type"] = _typeMap[t];
                else if (typeof(IEnumerable).IsAssignableFrom(t))
                {
                    p["type"] = "array";

                    var itemType = t.GetElementType() ?? t.GetGenericArguments().FirstOrDefault();

                    if (itemType != null)
                    {
                        var itemDesc = new Dictionary<string, object>();
                        MapModelType(itemDesc, itemType);
                        var hash = new Hashtable {{"$ref", itemType.Name}};
                        p["items"] = hash;
                    }
                }
                else if (t.IsEnum)
                {
                    p["type"] = "string";
                    p["allowableValues"] =
                        new
                            {
                                valueType = "LIST",
                                values = (from object v in Enum.GetValues(t) select v.ToString()).ToArray()
                            };
                }
                else
                    p["type"] = t.Name;
            }
            catch
            {
                throw new ArgumentException(string.Format("Cannot map type '{0}' to Swagger type", t));
            }
        }

        static void AccumulateAllReferencedTypes(Type t, HashSet<Type> typeAccumulator)
        {
            var underlying = Nullable.GetUnderlyingType(t);
            if (underlying != null)
                t = underlying;

            if (t.IsEnum || _typeMap.ContainsKey(t))
                return;

            if (typeof(IEnumerable).IsAssignableFrom(t))
                t = t.GetElementType() ?? t.GetGenericArguments().FirstOrDefault();

            if (typeAccumulator.Add(t))
            {
                foreach (var prop in t.GetProperties())
                {
                    if (prop.CanRead && prop.GetCustomAttributes(typeof(DataMemberAttribute), false).Length > 0)
                        AccumulateAllReferencedTypes(prop.PropertyType, typeAccumulator);
                }
            }
        }


        static Dictionary<string, object> GetModelProperties(Type t)
        {
            var underlying = Nullable.GetUnderlyingType(t);
            if (underlying != null)
            {
                t = underlying;
            }

            var result = new Dictionary<string, object>();
            foreach (var prop in t.GetProperties())
            {
                if (prop.CanRead && prop.GetCustomAttributes(typeof(DataMemberAttribute), false).Length > 0)
                {
                    var p = new Dictionary<string, object>();
                    var desc = GetDesc(prop);
                    if (!string.IsNullOrEmpty(desc))
                        p["description"] = desc;

                    MapModelType(p, prop.PropertyType);

                    result[prop.Name] = p;
                }
            }
            return result;
        }


        public static Dictionary<string, Dictionary<string, object>> GetModel(Type[] types)
        {
            var result = new Dictionary<string, Dictionary<string, object>>();
            var typeAccumulator = new HashSet<Type>();

            foreach (var type in types)
                AccumulateAllReferencedTypes(type, typeAccumulator);
            
            foreach (var t in typeAccumulator)
            {
                if (t.IsEnum)
                    continue;
                var properties = GetModelProperties(t);
                if (properties != null)
                {
                    result[t.Name] = new Dictionary<string, object>
					{ 
						{ "properties", properties },
						{ "id", t.Name }
					};
                }
            }

            return result;
        }

        #endregion
    }
}
