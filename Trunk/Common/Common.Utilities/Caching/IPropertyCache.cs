using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public interface IPropertyCache
    {
        // Methods
        IEnumerable<PropertyInfo> GetProperties(Type type);
        IEnumerable<PropertyInfo> GetReadOnlyProperties(Type type);
        IEnumerable<PropertyInfo> GetWriteOnlyProperties(Type type);
    }

 

}
