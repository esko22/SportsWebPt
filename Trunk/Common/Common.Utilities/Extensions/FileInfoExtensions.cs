using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class FileInfoExtensions
    {
        public static string GetContents(this FileInfo file)
        {
            return file.GetContents(false);
        }

        public static string GetContents(this FileInfo file, bool throwWhenFileMissing)
        {
            string fileContents = string.Empty;
            if (file != null && File.Exists(file.FullName))
            {
                fileContents = File.ReadAllText(file.FullName);
            }
            else if(throwWhenFileMissing)
            {
                throw new FileNotFoundException(TextResources.FileNotFound.FormatWith(file == null ? "unknown" : file.FullName ));
            }
            return fileContents;
        }

        public static string NameWithoutExtension(this FileInfo file)
        {
            if (string.IsNullOrWhiteSpace(file.Extension))
            {
                return file.Name;
            }

            var index = file.Name.LastIndexOf(file.Extension);

            var name = file.Name.Remove(index);
            return name;

        }
    }
}
