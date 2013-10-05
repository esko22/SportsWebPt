using System;
using System.IO;
using System.IO.Compression;

using ServiceStack.Text;

namespace SportsWebPt.Common.ServiceStack
{
    public static class JsonSerialization
    {
        public static byte[] CompressAndSerialize<T>(T value)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Compress, true))
                    {
                        JsonSerializer.SerializeToStream(value, typeof(T), ds);
                    }
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static T DeSerializeAndDecompress<T>(byte[] serializedValue)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(serializedValue))
                {
                    using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Decompress, true))
                    {
                        return (T)JsonSerializer.DeserializeFromStream(typeof(T), ds);
                    }
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public static T DeSerialize<T>(byte[] serializedValue)
        {
            using (MemoryStream ms = new MemoryStream(serializedValue))
            {
                return JsonSerializer.DeserializeFromStream<T>(ms);
            }
        }

        public static byte[] Serialize<T>(T value)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    JsonSerializer.SerializeToStream(value, typeof(T), ms);
                    return ms.ToArray();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
