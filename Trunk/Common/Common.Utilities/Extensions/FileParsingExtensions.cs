using System;
using System.IO;
using System.Linq;

namespace SportsWebPt.Common.Utilities
{
    public static class FileParsingExtensions
    {

        /// <summary>
        /// Tries the column split.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="delimiters">The delimiters.</param>
        /// <param name="expectedColumnCount">The expected column count.</param>
        /// <returns></returns>
        public static String[] TryColumnSplit(this String value, String[] delimiters, Int32 expectedColumnCount)
        {
            var splitValues = value.Split(delimiters, StringSplitOptions.None);

            if (splitValues.Length != expectedColumnCount)
                throw new Exception("Column split result does not match column definition");

            return splitValues;
        }

        /// <summary>
        /// Gets a file from a FileInfo Array for a given filename that does not have an extension.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="directoryFiles">The directory files.</param>
        /// <returns></returns>
        public static FileInfo GetDirectoryFileByName(this String fileName, FileInfo[] directoryFiles)
        {
            return (from directoryFile in directoryFiles
                    let fileNameWithoutExtension = Path.GetFileNameWithoutExtension(directoryFile.Name)
                    where !String.IsNullOrEmpty(fileNameWithoutExtension) && fileNameWithoutExtension.Equals(fileName, StringComparison.OrdinalIgnoreCase)
                    select directoryFile).FirstOrDefault();
        }
    }
}
