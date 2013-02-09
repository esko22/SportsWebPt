using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    internal static class TypeExtensions
    {
        internal static readonly Type[] PredefinedTypes = new Type[] { 
        typeof(object), typeof(bool), typeof(char), typeof(string), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal), typeof(DateTime), 
        typeof(TimeSpan), typeof(Guid), typeof(Math), typeof(Convert)
     };

        internal static bool IsPredefinedType(this Type type)
        {
            return PredefinedTypes.Any(type2 => type2 == type);
        }

        internal static bool IsNullableType(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        internal static Type GetNullableUnderlyingTypeIfExists(this Type type){
            if(type.IsNullableType()){
                return new NullableConverter(type).UnderlyingType;
            }
            return type;
        }
       

    }
}
