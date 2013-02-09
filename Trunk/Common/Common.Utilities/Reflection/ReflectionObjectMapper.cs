using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// TODO: convert this to an extension method
    /// based on an interface
    /// </summary>
    public class ReflectionObjectMapper
    {
        /// <summary>
        /// Method used to convert data transfer objects
        /// and business entities to and from based on
        /// identical property names.
        /// </summary>
        /// <typeparam name="TConversionToObject">The type of the conversion to object.</typeparam>
        /// <typeparam name="TConversionFromObject">The type of the conversion from object.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="constructorParams">The constructor params.</param>
        /// <returns></returns>
        public static TConversionToObject TransferObjectConversion<TConversionToObject, TConversionFromObject>(TConversionFromObject instance, Object[] constructorParams)
        {
            var convertToType = typeof(TConversionToObject);
            var convertToObjectInstance = (TConversionToObject)Activator.CreateInstance(convertToType, constructorParams);
            var convertToPropertyList = convertToType.GetProperties();
            var instanceProperties = ConvertPropertiesToDictionary<TConversionFromObject>(instance);

            foreach (var info in
                convertToPropertyList.Where(info => instanceProperties.ContainsKey(info.Name)).Where(info => (instanceProperties[info.Name] != null) 
                    && (info.PropertyType == instanceProperties[info.Name].GetType())))
            {
                info.SetValue(convertToObjectInstance, instanceProperties[info.Name], null);
            }

            return convertToObjectInstance;
        }

        /// <summary>
        /// Method used to convert data transfer objects
        /// and business entities to and from based on 
        /// identical property names.
        /// </summary>
        /// <typeparam name="TConversionToObject">The type of the conversion to object.</typeparam>
        /// <typeparam name="TConversionFromObject">The type of the conversion from object.</typeparam>
        /// <returns></returns>
        public static TConversionToObject TransferObjectConversion<TConversionToObject, TConversionFromObject>(TConversionFromObject instance)
        {
            return TransferObjectConversion<TConversionToObject, TConversionFromObject>(instance, null);
        }

        /// <summary>
        /// Converts the properties to dictionary.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static Dictionary<String, Object> ConvertPropertiesToDictionary<TObject>(TObject instance)
        {
            var type = typeof(TObject);
            var propertyList = type.GetProperties();

            return propertyList.ToDictionary(info => info.Name, info => info.GetValue(instance, null));
        }

    }
}
