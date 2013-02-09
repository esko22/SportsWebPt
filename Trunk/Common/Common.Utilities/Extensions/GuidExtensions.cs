using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class GuidExtensions
    {
        [DebuggerStepThrough]
        public static string Shrink(this Guid target)
        {
            Check.Argument.IsNotEmpty(target, "target");

            string base64 = Convert.ToBase64String(target.ToByteArray());

            string encoded = base64.Replace("/", "_").Replace("+", "-");

            return encoded.Substring(0, 22);
        }

        [DebuggerStepThrough]
        public static bool IsEmpty(this Guid target)
        {
            return target == Guid.Empty;
        }
    }
}
