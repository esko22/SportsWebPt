using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class TemplateColumnDataBuilder<T>: ColumnBuilderBase<T, TemplateColumnDataBuilder<T>> where T: class
    {
        public TemplateColumnDataBuilder(ColumnData<T> column) : base(column) {}
    }
}
