using System;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionStack(this Exception ex)
        {
            return GetExceptionStack(ex, true);
        }

        public static string GetExceptionStack(this Exception ex, Boolean onlyIfInnerExist)
        {
            if (onlyIfInnerExist && ex.InnerException == null)
                return String.Empty;

            var exceptionBuilder = new StringBuilder();
            RecurseException(exceptionBuilder, ex);

            return exceptionBuilder.ToString();
        }

        private static void RecurseException(StringBuilder exceptionBuilder, Exception ex)
        {
            exceptionBuilder.Append(String.Format("Exception --> {0} ", ex.Message));

            if (ex.InnerException != null)
            {
                RecurseException(exceptionBuilder, ex.InnerException);
            }
        }
    }
}
