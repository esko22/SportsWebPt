using System;
using System.ComponentModel;

namespace SportsWebPt.Common.Utilities.ServiceApi
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiOperationAttribute : DescriptionAttribute
    {
        #region Properties

        public String Resource { get; set; }

        public String Path { get; set; }

        public String HttpMethod { get; set; }

        public String Summary { get; set; }

        public String Nickname { get; set; }

        #endregion
    }
}
