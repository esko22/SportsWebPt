using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;


namespace SportsWebPt.Common.Utilities {
    public class XslTransformer  {

        /// <summary>
        /// Transforms the specified xml using supplied xsl.
        /// </summary>
        /// <param name="xslt">The XSLT.</param>
        /// <param name="xml">The XML.</param>
        /// <exception cref="T:System.Exception"></exception>
        /// <exception cref="T:System.Xml.XmlException"></exception>
        /// <exception cref="T:System.Xml.Xsl.XsltException"></exception>
        /// <returns></returns>
        public string Transform(string xslt, string xml) {
            string rVal = "";
            try {
                xml = xml.Trim();
                using (var reader = new XmlTextReader(new StringReader(xslt))) {
                    using (var xmlReader = new XmlTextReader(new StringReader(xml))) {
                        using (var stringWriter = new StringWriter()) {
                            using (var transformedXml = new XmlTextWriter(stringWriter)) {
                                var xsltrans = new XslCompiledTransform();
                                xsltrans.Load(reader);
                                xsltrans.Transform(xmlReader, transformedXml);
                                rVal = stringWriter.ToString();
                            }
                        }
                    }
                }
            }
            catch (XmlException xmlEx) {
                throw;
            }
            catch (XsltException xsltEx) {
                throw;
            }
            catch (Exception ex) {
                throw;
            }
            return rVal;

        }
    }
}
