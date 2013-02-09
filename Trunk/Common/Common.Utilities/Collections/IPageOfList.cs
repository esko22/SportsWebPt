using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public interface IPageOfList<T> : IList<T>
    {
        int PageIndex { get; }
        int PageNumber { get; }
        int PageSize { get; }
        int TotalPageCount { get; }
        int TotalItemCount { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
    }
}
