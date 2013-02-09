using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class PageRequest
    {
        private int pageNumber = 1;
        private static int defaultPageSize = 20;
        private int pageSize = defaultPageSize;

        /// <summary>
        /// 1 based page number, default is 1
        /// </summary>
        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                if (value > 0)
                {
                    pageNumber = value;
                }
            }

        }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value > 0)
                {
                    pageSize = value;
                }
            }
        }

        public string WhereExpression { get; set; }
        
        public object[] WhereValues { get; set; }

        public bool IsDescending { get; set; }

        public string OrderBy { get; set; }

        /// <summary>
        /// Zero base paging
        /// </summary>
        public int PageIndex
        {
            get { return PageNumber - 1; }
        }

        public int SkipTo
        {
            get
            {
                return PageSize * PageIndex;
            }
        }

        public PageRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PageRequest(int? pageNumber, int? pageSize, int defaultPageSize = 20)
        {

            defaultPageSize = defaultPageSize > 0 ? defaultPageSize : 20;
            PageNumber = pageNumber.HasValue && pageNumber.Value > 0 ? pageNumber.Value : 1;
            PageSize = pageSize.HasValue && pageSize.Value > 0 ? pageSize.Value : defaultPageSize;
        }
    }
}
