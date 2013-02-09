using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace SportsWebPt.Common.Utilities
{
    public class SchemaValidationMessage
    {
        public XmlSchemaException SchemaException { get; private set; }
        public XmlSeverityType Severity { get; private set; }
        public string Message { get; private set; }

        public SchemaValidationMessage(ValidationEventArgs eventArgs) :this(eventArgs.Exception, eventArgs.Severity, eventArgs.Message)
        {
        }

        public SchemaValidationMessage(XmlSchemaException exception,XmlSeverityType severity, string message)
        {
            this.SchemaException = exception;
            this.Severity = severity;
            this.Message =message;
        }

        public SchemaValidationMessage(string message): this(null, XmlSeverityType.Error, message)
        {
        }
        
    }
}
