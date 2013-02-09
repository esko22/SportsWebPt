using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

namespace SportsWebPt.Common.Utilities
{
    public static class SerializationExtensions
    {
        /// <summary>
        ///   Converts the object to binary.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "o">The o.</param>
        /// <exception cref = "T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the System.Exception.InnerException property.</exception>
        /// <returns></returns>
        public static byte[] ToBinary<T>(this T o) where T : class, new()
        {
            byte[] result = null;
            if (o != null)
            {
                var bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, o);
                    result = ms.ToArray();
                }
            }
            return result;
        }

        /// <summary>
        ///   Converts the object from binary.
        /// </summary>
        /// <typeparam name = "TResult">The type of the result.</typeparam>
        /// <param name = "input">The input.</param>
        /// <param name = "bits">The bits.</param>
        /// <returns></returns>
        public static TResult FromBinary<TResult>(this TResult input, byte[] bits) where TResult : class, new()
        {
            TResult obj;
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(bits, 0, bits.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                obj = (TResult) binForm.Deserialize(memStream);
            }
            return obj;
        }

        /// <summary>
        ///   Serializes the object to XML string
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "o">The o.</param>
        /// <exception cref = "T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the System.Exception.InnerException property.</exception>
        /// <returns></returns>
        public static string ToXml<T>(this T o) where T : class, new()
        {
            return o.ToXml(false);
        }


        /// <summary>
        ///   Serializes the object to XML string
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "o">The o.</param>
        /// <param name = "stripXmlTag">if set to <c>true</c> [strip XML tag].</param>
        /// <returns></returns>
        /// <exception cref = "T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the System.Exception.InnerException property.</exception>
        public static string ToXml<T>(this T o, bool stripXmlTag) where T : class, new()
        {
            string rVal;
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
                {
                    
                    var xs = new XmlSerializer(typeof (T));
                    xs.Serialize(xmlTextWriter, o);

                    rVal = memoryStream.ToArray().ToUTF8String();
                }
            }
            if (stripXmlTag)
            {
                rVal = rVal.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            }
            return rVal.Trim();
        }

        public static string ToXmlNoGeneric(this object o, bool stripXmlTag)
        {
            string rVal;
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
                {

                    var xs = new XmlSerializer(o.GetType());
                    xs.Serialize(xmlTextWriter, o);

                    rVal = memoryStream.ToArray().ToUTF8String();
                }
            }
            if (stripXmlTag)
            {
                rVal = rVal.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            }
            return rVal.Trim();
        }

        public static TResult FromXml<TResult>(this string xml)
        {
            TResult rVal = default(TResult);

            if (!string.IsNullOrEmpty(xml))
            {
                using (var memoryStream = new MemoryStream(xml.ToUtf8ByteArray()))
                {
                    var xs = new XmlSerializer(typeof(TResult));
                    rVal = (TResult)xs.Deserialize(memoryStream);
                }
            }
            return rVal;
        }

        /// <summary>
        ///   Deserializes object from xml
        /// </summary>
        /// <typeparam name = "TResult">The type of the result.</typeparam>
        /// <param name = "input">The input.</param>
        /// <param name = "xml">The bits.</param>
        /// <returns></returns>
        public static TResult FromXml<TResult>(this TResult input, string xml) where TResult : class, new()
        {
            TResult rVal = default(TResult);

            if (!string.IsNullOrEmpty(xml))
            {
                using (var memoryStream = new MemoryStream(xml.ToUtf8ByteArray()))
                {
                    var xs = new XmlSerializer(typeof (TResult));
                    rVal = (TResult) xs.Deserialize(memoryStream);
                }
            }
            return rVal;
        }

       
    }
}