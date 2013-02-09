using System;
using System.ComponentModel;

namespace Illumina.Common.ServiceStack.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiResourceAttribute : DescriptionAttribute
    {

        #region Properties

        public String Path { get; private set; }

        public String Category { get; private set; }

        #endregion

        #region Construction

        public ApiResourceAttribute(String description, String category, String path)
            : base(description)
        {
            Category = category;
            Path = path;
        }

        #endregion
    }
}
