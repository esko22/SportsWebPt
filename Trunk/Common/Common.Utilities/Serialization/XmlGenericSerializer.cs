using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class XmlGenericSerializer : IGenericSerializer
    {
        public XmlGenericSerializer()
            : this(false)
        {

        }

        public XmlGenericSerializer(bool stripXmlRootTag)
        {
            StripXmlRootTag = stripXmlRootTag;
        }

        public bool StripXmlRootTag { get; set; }

        public string Serialize(object input)
        {
            return input.ToXml(StripXmlRootTag);
        }

        public T Deserialize<T>(string input)
        {
            return input.FromXml<T>();
        }
    }
}
