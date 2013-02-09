using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.IO.IsolatedStorage;

namespace SportsWebPt.Common.Utilities
{
    public static class SerilaizationHelper<T> where T : class
    {
        public static string Serialize(T obj)
        {
            string result = string.Empty;
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                dcs.WriteObject(ms, obj);

                result = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Position);
            }
            return result;
        }

        public static T Deserialize(string serialized)
        {
            if (serialized == null) throw new ArgumentNullException("serialized");
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));

            T result = null;
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(serialized)))
            {
                result = dcs.ReadObject(ms) as T;
            }

            return result;
        }

        public static T DeserializeFromDisk(string path)
        {
            T result = default(T);

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);

                result = SerilaizationHelper<T>.Deserialize(sr.ReadToEnd());
            }

            return result;
        }

        public static void SerializeObjectToDisk(string path, T obj)
        {
            File.WriteAllText(path, SerilaizationHelper<T>.Serialize(obj));
        }

       
        public static void SerializeObjectToIsolatedStorage(string name, T obj)
        {
            using (var isFileStream = new IsolatedStorageFileStream(name, FileMode.Truncate))
            {
                using (var writer = new StreamWriter(isFileStream))
                {
                    writer.Write(SerilaizationHelper<T>.Serialize(obj));
                }
            }
            
        }

        public static T DeserializeFromIsolatedStorage(string name)
        {
            T result = default(T);

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly, null, null);
            if (!isoStore.FileExists(name))
            {
                return null;
            }

            using (var isFileStream = new IsolatedStorageFileStream(name, FileMode.Open))
            {
                using (var reader = new StreamReader(isFileStream))
                {
                    result = SerilaizationHelper<T>.Deserialize(reader.ReadToEnd());
                }
            }

            return result;
        }
    }
}
