using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class StreamReaderExtensions
    {
        /// <summary>
        /// Resets the streamreader back to the beginning of the stream, similar to Position = 0 of a normal stream
        /// </summary>
        /// <param name="reader"></param>
        public static void ResetToBeginning(this StreamReader reader)
        {
            if(reader != null)
            {
                reader.DiscardBufferedData();
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                reader.BaseStream.Position = 0;
            }
        }
    }
}
