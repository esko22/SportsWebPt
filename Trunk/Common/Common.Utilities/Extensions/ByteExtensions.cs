
using System;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class ByteExtensions
    {
        public static string ToUTF8String(this byte[] characters)
        {
            var encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }
    }
}