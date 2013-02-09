using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    /// <remarks>
    /// - ideas from http://michaelsync.net/2009/04/09/silverlightwpf-implementing-propertychanged-with-expression-tree
    /// </remarks>
    public static class PropertyChangedExtensions
    {
        private const string SELECTOR_MUSTBEPROP = "PropertySelector must select a property type.";

        public static void Notify<T>(this Action<string> notifier, Expression<Func<T>> propertySelector)
        {
            if (notifier != null)
                notifier(GetPropertyName(propertySelector));
        }

        public static void Notify<T>(this PropertyChangedEventHandler handler, Expression<Func<T>> propertySelector)
        {
            if (handler != null)
            {
                var memberExpression = GetMemberExpression(propertySelector);
                if (memberExpression != null)
                {
                    var _sender = ((ConstantExpression)memberExpression.Expression).Value;
                    handler(_sender, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
            //else we don't raise error for handler == null, because it is null when no has attached to the event..
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertySelector)
        {
            var _memberExpression = propertySelector.Body as MemberExpression;
            if (_memberExpression == null)
            {
                var _unaryExpression = propertySelector.Body as UnaryExpression;
                if (_unaryExpression != null) _memberExpression = _unaryExpression.Operand as MemberExpression;
            }
            if (_memberExpression != null)
            {
                Check.Argument.IsNotTrue((_memberExpression.Member.MemberType != MemberTypes.Property), SELECTOR_MUSTBEPROP);
                return _memberExpression.Member.Name;
            }
            return null;
        }

        public static MemberExpression GetMemberExpression<T>(Expression<Func<T>> propertySelector)
        {
            var _memberExpression = propertySelector.Body as MemberExpression;
            if (_memberExpression != null)
            {
                Check.Argument.IsNotTrue((_memberExpression.Member.MemberType != MemberTypes.Property),SELECTOR_MUSTBEPROP);
                return _memberExpression;
            }

            // for WPF
            var _unaryExpression = propertySelector.Body as UnaryExpression;
            if (_unaryExpression != null)
            {
                var _innerMemberExpression = _unaryExpression.Operand as MemberExpression;
                if (_innerMemberExpression != null)
                {
                    Check.Argument.IsNotTrue((_innerMemberExpression.Member.MemberType != MemberTypes.Property), SELECTOR_MUSTBEPROP);
                    return _innerMemberExpression;
                }
            }

            // all else
            return null;
        }
    }
}
