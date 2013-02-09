using System;
using System.ComponentModel;

namespace SportsWebPt.Common.Utilities.ServiceApi
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiResourceAttribute : DescriptionAttribute
    {

        #region Properties

        public String Path { get; private set; }

        public String Name { get; private set; }

        public String Category { get; private set; }

        #endregion

        #region Construction

        public ApiResourceAttribute(String description, String name, String path)
            : this(description, name, path, "General")
        {;}

        public ApiResourceAttribute(String description, String name, String path, String category)
            : base(description)
        {
            Name = name;
            Path = path;
            Category = category;
        }

        #endregion
    }
}
