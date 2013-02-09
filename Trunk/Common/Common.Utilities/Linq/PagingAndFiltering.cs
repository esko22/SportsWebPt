using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SportsWebPt.Common.Utilities
{
    public static class PagingAndFiltering
    {
        /// <summary>
        /// Filters the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="whereExpression">The where expression. "columname == @0 && columname2 == @1"</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="isDescending">if set to <c>true</c> [is descending].</param>
        /// <param name="whereValues">The where values.</param>
        /// <returns></returns>
        public static IQueryable<T> Filter<T>(this IQueryable<T> source, string whereExpression, string orderBy, bool isDescending, params object[] whereValues){
            return (IQueryable<T>) Filter((IQueryable) source, whereExpression, orderBy, isDescending, whereValues);
        }

        public static IQueryable Filter(this IQueryable source, string whereExpression, string orderBy, bool isDescending, params object[] whereValues){
            Check.Argument.IsNotNull(source, "source");
            

            if (!string.IsNullOrEmpty(whereExpression) && !string.IsNullOrEmpty(orderBy))
            {
                return source.Where(whereExpression, whereValues).OrderBy(orderBy + (isDescending ? " desc" : ""), null);
            }

            if (string.IsNullOrEmpty(whereExpression) && !string.IsNullOrEmpty(orderBy))
            {
                return source.OrderBy(orderBy + (isDescending ? " desc" : ""), null);
            }

            if (!string.IsNullOrEmpty(whereExpression))
            {
                return source.Where(whereExpression, whereValues);
            }

            return source;            
        }

        public static IQueryable Filter(this IQueryable source, string whereExpression, string orderByExpression, params object[] whereValues)
        {
            Check.Argument.IsNotNull(source, "source");
           
            if (!string.IsNullOrEmpty(whereExpression) && !string.IsNullOrEmpty(orderByExpression))
            {
                return source.Where(whereExpression, whereValues).OrderBy(orderByExpression, null);
            }

            if (string.IsNullOrEmpty(whereExpression) && !string.IsNullOrEmpty(orderByExpression))
            {
                return source.OrderBy(orderByExpression, null);
            }

            if (!string.IsNullOrEmpty(whereExpression))
            {
                return source.Where(whereExpression, whereValues);
            }

            return source;
        }

        /// <summary>
         /// Pages the specified source.
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="source">The source.</param>
         /// <param name="skip">The skip.</param>
         /// <param name="take">The take.</param>
         /// <returns></returns>
         public static IQueryable<T> Page<T>(this IQueryable<T> source, int skip, int take)
        {
            return (IQueryable<T>)Page((IQueryable)source, skip, take);
        }

         public static IQueryable Page(this IQueryable source, int skip, int take){
             Check.Argument.IsNotNull(source, "source");
             return source.Skip(skip).Take(take);
         }


         public static PageOfList<T> CreatePageOfList<T>(Func<IQueryable<T>> action, PageRequest request)
         {
             return CreatePageOfList(action, request, false);
         }

         public static PageOfList<T> CreatePageOfList<T>(Func<IQueryable<T>> action, PageRequest request, bool queryHasAlreadyExecuted)
         {
             if (queryHasAlreadyExecuted)
             {
                 IQueryable<T> query = action.Invoke();

                 query = query.Filter(request.WhereExpression, request.OrderBy, request.IsDescending, request.WhereValues);
                 var count = query.Count();
                 var results = query.Page(request.SkipTo, request.PageSize).ToList();
                 return new PageOfList<T>(results, request.PageNumber, request.PageSize, count);
             }
             else
             {
                 IQueryable<T> query = action.Invoke();

                 query = query.Filter(request.WhereExpression, request.OrderBy, request.IsDescending, request.WhereValues);
                 IQueryable<T> countQuery;
                 countQuery = action.Invoke();

                 var count = countQuery.Filter(request.WhereExpression, request.OrderBy, request.IsDescending, request.WhereValues).Count();

                 var results = query.Page(request.SkipTo, request.PageSize).ToList();

                 return new PageOfList<T>(results, request.PageNumber, request.PageSize, count);
             }
         }
    }
}
