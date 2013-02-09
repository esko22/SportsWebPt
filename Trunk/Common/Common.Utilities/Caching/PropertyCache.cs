using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class PropertyCache : AbstractCacheBase<RuntimeTypeHandle, IEnumerable<PropertyInfo>>, IPropertyCache
    {
        // Methods
        private static Func<bool> CreateMatchingGet(PropertyInfo property)
        {
            Func<bool> hasGet = delegate
            {
                MethodInfo getMethod = property.GetGetMethod();
                return (getMethod != null) && (getMethod.GetParameters().Length == 0);
            };
            return delegate
            {
                if (property.CanRead)
                {
                    return hasGet();
                }
                return false;
            };
        }

        private static Func<bool> CreateMatchingSet(PropertyInfo property)
        {
            Func<bool> hasSet = delegate
            {
                MethodInfo setMethod = property.GetSetMethod();
                return (setMethod != null) && (setMethod.GetParameters().Length == 1);
            };
            return delegate
            {
                if (property.CanWrite)
                {
                    return hasSet();
                }
                return false;
            };
        }

        private IEnumerable<PropertyInfo> GetInternalProperties(Type type)
        {
            Func<PropertyInfo, bool> canInclude = delegate(PropertyInfo property)
            {
                if (!CreateMatchingGet(property)())
                {
                    return CreateMatchingSet(property)();
                }
                return true;
            };
            return base.GetOrCreate(type.TypeHandle, delegate
            {
                return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(canInclude);
            });
        }

        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            Check.Argument.IsNotNull(type, "type");
            Func<PropertyInfo, bool> predicate = delegate(PropertyInfo property)
            {
                if (CreateMatchingGet(property)())
                {
                    return CreateMatchingSet(property)();
                }
                return false;
            };
            return this.GetInternalProperties(type).Where(predicate);
        }

        public IEnumerable<PropertyInfo> GetReadOnlyProperties(Type type)
        {
            Check.Argument.IsNotNull(type, "type");
            Func<PropertyInfo, bool> predicate = delegate(PropertyInfo property)
            {
                return CreateMatchingGet(property)();
            };
            return this.GetInternalProperties(type).Where<PropertyInfo>(predicate);
        }

        public IEnumerable<PropertyInfo> GetWriteOnlyProperties(Type type)
        {
            Check.Argument.IsNotNull(type, "type");
            Func<PropertyInfo, bool> predicate = property => CreateMatchingSet(property)();
            return this.GetInternalProperties(type).Where(predicate);
        }
        
    }


}
