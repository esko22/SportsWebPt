using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class ObjectExtensions
    {
        public static bool IsNumeric(this Object obj)
        {
            if (obj == null || obj is DateTime)
                return false;

            if (obj is Int16 || obj is Int32 || obj is Int64 || obj is Decimal || obj is Single || obj is Double || obj is Boolean)
                return true;

            try
            {
                if (obj is string)
                    Double.Parse(obj as string);
                else
                    Double.Parse(obj.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }
    }

}
