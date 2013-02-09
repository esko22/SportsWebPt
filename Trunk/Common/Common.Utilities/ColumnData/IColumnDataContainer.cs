using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public interface IColumnDataContainer<T> where T: class
    {
        IList<ColumnData<T>> Columns { get; }
    }
}
