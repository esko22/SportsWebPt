using System;
using System.IO;
using System.IO.Compression;

using ProtoBuf;

namespace SportsWebPt.Common.ServiceStack
{
    public static class ProtoBufSerialization
    {
        public static byte[] CompressAndSerialize<T>(T value)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Compress, true))
                    {
                        Serializer.Serialize(ds, value);
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
                        return (T)Serializer.Deserialize<T>(ds);
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
                return Serializer.Deserialize<T>(ms);
            }
        }

        public static byte[] Serialize<T>(T value)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize(ms, value);
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
