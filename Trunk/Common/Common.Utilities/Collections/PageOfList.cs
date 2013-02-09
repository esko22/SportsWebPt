using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
     public class PageOfList<T> : List<T>, IPageOfList<T>
    {
         /// <summary>
         /// Paged List of items
         /// </summary>
         /// <param name="items"></param>
         /// <param name="page">1 based page number</param>
         /// <param name="pageSize"></param>
         /// <param name="totalItemCount"></param>
        public PageOfList(IEnumerable<T> items, int page, int pageSize, int totalItemCount)
        {
            if (items != null)
                AddRange(items);

            PageIndex = page - 1;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }

        public PageOfList() { }

        #region IPageOfList<T> Members

        public int PageItemStart
        {
            get
            {
                var rVal = PageIndex * PageSize;
                return rVal <= 0 ? 1 : rVal;
            }
        }

        public int PageItemEnd
        {
            get
            {
                var rVal = PageIndex * PageSize + PageSize;
                return rVal > TotalItemCount ? TotalItemCount : rVal;
            }
        }

         /// <summary>
         /// Zero based page number
         /// </summary>
        public int PageIndex { get; private set; }

         /// <summary>
         /// 1 based page number
         /// </summary>
        public int PageNumber
        {
            get
            {
                return PageIndex + 1;
            }
        }

        public int PageSize { get; private set; }

        public int TotalItemCount { get; private set; }

        public bool HasNextPage
        {
            get
            {
                return PageIndex + 1 != TotalPageCount;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 0;
            }
        }
        
        public int TotalPageCount { get; private set; }


        #endregion
    }
}
