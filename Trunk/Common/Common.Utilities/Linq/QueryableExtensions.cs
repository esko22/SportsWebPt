﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class QueryableExtensions
    {

        public static IQueryable<TElement> Distinct<TElement>(this IQueryable<TElement> source, Func<TElement, TElement, bool> comparer)
        {
            return source.Distinct(new LambdaComparer<TElement>(comparer));
        }
       

        /// <summary> 
        /// Return the element that the specified property's value is contained in the specific values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, params TValue[] values)
        {
        
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

       
        /// <summary> 
        /// Return the element that the specified property's value is contained in the specific values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }


        /// <summary>
        ///Return the element that the specified property's value is not contained in the specific values 
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static IQueryable<TElement> WhereNotIn<TElement, TValue>(this IQueryable<TElement> source,
            Expression<Func<TElement, TValue>> propertySelector,
            params TValue[] values)
        {
            return source.Where(GetWhereNotInExpression(
                propertySelector, values));
        }

        /// <summary>
        /// Return the element that the specified property's value is not contained in the specific values 
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static IQueryable<TElement> WhereNotIn<TElement, TValue>(this IQueryable<TElement> source,
            Expression<Func<TElement, TValue>> propertySelector,
            IEnumerable<TValue> values)
        {
            return source.Where(GetWhereNotInExpression(
                propertySelector, values));
        }


        private static Expression<Func<TElement, bool>> GetWhereNotInExpression<TElement, TValue>(
    Expression<Func<TElement, TValue>> propertySelector,
    IEnumerable<TValue> values)
        {
            ParameterExpression p =
                propertySelector.Parameters.Single();

            if (!values.Any())
            {
                return e => true;
            }

            var unequals = values.Select(value =>
                (Expression)Expression.NotEqual(
                    propertySelector.Body,
                    Expression.Constant(value, typeof(TValue))
                )
            );

            var body = unequals.Aggregate((accumulate, unequal) => Expression.And(accumulate, unequal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        private static Expression<Func<TElement, bool>> GetWhereInExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            if (!values.Any())
                return e => false;

            var equals = values.Select(value => (Expression)Expression.Equal(propertySelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
    } 
}
