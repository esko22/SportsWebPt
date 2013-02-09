using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public interface IConfigurationManager
    {
        NameValueCollection AppSettings
        {
            get;
        }

        string ConnectionStrings(string name);

        T GetSection<T>(string sectionName);
    }
}
