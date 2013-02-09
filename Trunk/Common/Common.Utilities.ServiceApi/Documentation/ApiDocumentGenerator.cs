using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SportsWebPt.Common.Utilities;

namespace SportsWebPt.Common.Utilities.ServiceApi
{
    public abstract class ApiDocumentGenerator
    {
        #region Fields

        private readonly IEnumerable<String> _assemblyNames;

        #endregion
        
        #region Properties

        public Dictionary<string, Hashtable> ApiResourceListing { get; protected set; }

        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> ApiOperationListing { get; protected set; }

        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> ApiModelListing { get; protected set; }

        #endregion

        #region Construction

        protected ApiDocumentGenerator(IEnumerable<String> assemblyNames)
        {
            Check.Argument.IsNotNull(assemblyNames, "assemblyNames");
            _assemblyNames = assemblyNames;
        }

        #endregion

        #region Methods

        public void Generate()
        {
            ApiResourceListing = new Dictionary<string, Hashtable>();
            ApiOperationListing = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
            ApiModelListing = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => _assemblyNames.Contains(a.GetName().Name)))
            {
                foreach (var type in assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(ApiResourceAttribute), true).Any()))
                {
                    var resourceName = BuildApiResourceDescription(type);
                    if (!String.IsNullOrEmpty(resourceName))
                        BuildApiResourceDeclaration(resourceName, type);
                }
            }
        }

        protected abstract string BuildApiResourceDescription(Type metaType);

        protected abstract void BuildApiResourceDeclaration(String resourceName, Type metaType);

        #endregion

    }
}
