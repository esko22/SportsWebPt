﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// See http://www.albahari.com/expressions for information and examples.
    /// http://www.albahari.com/nutshell/predicatebuilder.aspx
    /// </summary>
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>(){
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>(){
            return f => false;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                      Expression<Func<T, bool>> expr2){
            InvocationExpression invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                    (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                       Expression<Func<T, bool>> expr2){
            InvocationExpression invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                    (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}