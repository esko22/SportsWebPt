using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SportsWebPt.Common.Utilities
{
    public static class ExpressionExtensions
    {
      

        public static string MemberWithoutInstance<T>(this Expression<Func<T, object>> expression) where T : class
        {
            MemberExpression expression2 = expression.ToMemberExpression<T>();
            if (expression2 == null)
            {
                return null;
            }
            if (expression2.Expression.NodeType == ExpressionType.MemberAccess)
            {
                var expression3 = (MemberExpression)expression2.Expression;
                return expression2.ToString().Substring(expression3.Expression.ToString().Length + 1);
            }
            return expression2.Member.Name;
        }

        public static MemberExpression ToMemberExpression<T>(this Expression<Func<T, object>> expression) where T : class
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                var expression3 = expression.Body as UnaryExpression;
                if (expression3 != null)
                {
                    body = expression3.Operand as MemberExpression;
                }
            }
            return body;
        }

        public static string LastMemberName<T>(this Expression<Func<T, object>> expression) where T : class
        {
            MemberExpression expression2 = expression.ToMemberExpression<T>();
            if (expression2 == null)
            {
                return null;
            }
            return expression2.Member.Name;
        }

        


 

 


 



    }
}
