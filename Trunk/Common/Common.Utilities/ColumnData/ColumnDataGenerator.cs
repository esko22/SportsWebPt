using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace SportsWebPt.Common.Utilities
{
    public class ColumnDataGenerator
    {
        #region Fields

        // Fields
        private readonly IPropertyCache propertyCache;

        #endregion Fields

        #region Constructors

      
        public ColumnDataGenerator(IPropertyCache propertyCache)
        {
            Check.Argument.IsNotNull(propertyCache, "propertyCache");
            this.propertyCache = propertyCache;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<ColumnData<T>> GetColumns<T>()where T : class
        {
            var roProps = this.propertyCache.GetReadOnlyProperties(typeof(T));
            
             return roProps.Where(property => (property.PropertyType.IsEnum ||
                    ((property.PropertyType != typeof (object)) && property.PropertyType.IsPredefinedType()) ||
                    (property.PropertyType.IsNullableType() && property.PropertyType.GetNullableUnderlyingTypeIfExists().IsPredefinedType())))
                .Select(property => CreateGetterExpression<T>(property))
                .Select(expression => new ColumnData<T>(expression));
        }

        private static Expression<Func<T, object>> CreateGetterExpression<T>(PropertyInfo property)
            where T : class
        {
            ParameterExpression expression;
            Expression expression2 = Expression.Property(expression = Expression.Parameter(typeof(T), "x"), property);
            if (property.PropertyType.IsValueType)
            {
                expression2 = Expression.Convert(expression2, typeof(object));
            }
            return Expression.Lambda<Func<T, object>>(expression2, new[] { expression });
        }

        #endregion Methods
    }
}