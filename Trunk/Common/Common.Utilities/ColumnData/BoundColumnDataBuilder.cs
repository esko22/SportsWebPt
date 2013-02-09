using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class BoundColumnDataBuilder<T>: ColumnBuilderBase<T, BoundColumnDataBuilder<T>> where T: class
    {
        public BoundColumnDataBuilder(ColumnData<T> column) : base(column) {}
        

    }
}
