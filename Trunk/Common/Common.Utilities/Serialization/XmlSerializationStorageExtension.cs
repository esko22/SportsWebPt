using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SportsWebPt.Common.Utilities
{
    public static class XmlSerializationStorageExtension
    {

        #region Methods

        public static String SerializeToSimpleFragment(this Object obj)
        {
            var collectionBuilder = new StringBuilder();

            var xmlSerializer = new XmlSerializer(obj.GetType());
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                NewLineOnAttributes = true,
                ConformanceLevel = ConformanceLevel.Auto
            };

            var blankNamespace = new XmlSerializerNamespaces();
            blankNamespace.Add(String.Empty, String.Empty);

            using (var metadataWriter = XmlWriter.Create(collectionBuilder, settings))
            {
                xmlSerializer.Serialize(metadataWriter, obj, blankNamespace);
            }

            return collectionBuilder.ToString();
        }

        public static List<TCollectionType> DeserializeGenericCollection<TCollectionType>(this String xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<TCollectionType>));

            using (TextReader reader = new StringReader(xml))
            {
                return xmlSerializer.Deserialize(reader) as List<TCollectionType>;
            }
        }

        public static TObjectType DeserializeGenericObject<TObjectType>(this String xml)
        {
            var xmlSerializer = new XmlSerializer(typeof (TObjectType));

            using (var reader = new StringReader(xml))
            {
                return (TObjectType) xmlSerializer.Deserialize(reader);
            }
        }

        #endregion
    }
}
