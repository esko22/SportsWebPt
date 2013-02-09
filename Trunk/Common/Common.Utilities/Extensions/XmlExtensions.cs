using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SportsWebPt.Common.Utilities
{
    public static class XmlExtensions
    {
        public static string ToSafeString(this XmlAttribute attribute, string defaultValue)
        {
            if(attribute == null)
            {
                return defaultValue;
            }
            return attribute.Value;
        }

        public static string ToSafeString(this XAttribute attribute, string defaultValue)
        {
            if (attribute == null)
            {
                return defaultValue;
            }
            return attribute.Value;
        }
    }
}
