using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Text.RegularExpressions;

namespace SportsWebPt.Common.Utilities
{
    public class XPathTextSubstitution
    {
        private readonly XmlDocument xmlDoc;

        /// <summary>
        /// Initializes a new instance of the <see cref="XPathTextSubstition"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <exception cref="T:System.ArgumentNullException">If xml is null or empty</exception>
        /// <exception cref="T:System.Xml.XmlException">If xml not well formed</exception>
        public XPathTextSubstitution(string xml)
        {
            xml = xml.Trim();
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException("xml");

            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XPathTextSubstition"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <exception cref="T:System.ArgumentNullException">If xml is null or empty</exception>
        public XPathTextSubstitution(XmlDocument xml)
        {
            if(xml == null)
                throw new ArgumentNullException("xml");
            this.xmlDoc = xml;
        }

        /// <summary>
        /// Replaces the string with xpath query
        /// </summary>
        /// <param name="textWithXPath">The text with X path.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        public string ReplaceWithXpath(string textWithXPath, string delimeter)
        {
            if (string.IsNullOrEmpty(textWithXPath))
                return textWithXPath;

            string output = textWithXPath;
            //test delimeter %{2}((\w)+)%{2}
            string pattern = delimeter + @"[^%]*" + delimeter;
            //extract all of the xpath expressions
            var collection = Regex.Matches(textWithXPath, pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            foreach (var match in collection)
            {
                string xpath = match.ToString().Replace(delimeter, "");
                string value = "";
                if (this.xmlDoc != null && this.xmlDoc.DocumentElement != null)
                {
                    var nodes = this.xmlDoc.DocumentElement.SelectNodes(xpath);
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            value += node.InnerText;
                        }
                    }
                }
                output = output.Replace(match.ToString(), value);
            }
            //concat all of them

            return output;
        }
    }
}
