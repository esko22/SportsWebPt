using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace SportsWebPt.Common.Utilities
{
    public class SchemaValidator 
    {
        private string xsd;
        private readonly XmlSchemaSet schemaSet;
        public List<SchemaValidationMessage> SchemaValidations { get; private set; }
        
        public SchemaValidator(string xsd)
        {
            this.xsd = xsd.Trim();
            if(string.IsNullOrEmpty(this.xsd))
                throw new ArgumentNullException("xsd");

            //create the schema set
            schemaSet = new XmlSchemaSet();
            
            var reader = new XmlTextReader(new StringReader(this.xsd));
            schemaSet.Add(null, reader);

            SchemaValidations = new List<SchemaValidationMessage>();
        }

        public bool TryValidateXml(string xml)
        {
            try
            {
                xml = xml.Trim();
                SchemaValidations.Clear();
                
                //load the xml
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlDoc.Schemas = schemaSet;

                //validate
                xmlDoc.Validate(SettingsValidationEventHandler);
               
                return SchemaValidations.Count == 0;
            }
            catch (Exception ex)
            {
                SchemaValidations.Add(new SchemaValidationMessage(ex.Message));
                return false;
            }
        }

        void SettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
           SchemaValidations.Add(new SchemaValidationMessage(e));
        }

        public static string StripOutNamespaces(string xml)
        {
            return Regex.Replace(xml, @"(xmlns:?[^=]*=[""][^""]*[""])",
                                 "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
    }
}
