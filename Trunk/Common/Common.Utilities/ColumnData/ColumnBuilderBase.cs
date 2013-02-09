using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public abstract class ColumnBuilderBase<T, TColumnBuilder>
        where T : class
        where TColumnBuilder : ColumnBuilderBase<T, TColumnBuilder>
    {
       
        protected ColumnBuilderBase(ColumnData<T> column)
        {
            Check.Argument.IsNotNull(column, "column");
            this.Column = column;
        }


        public ColumnData<T> Column
        {
            get; set;
        }

       
        public virtual TColumnBuilder Name(string name)
        {
            this.Column.Name = name;
            return (this as TColumnBuilder);
        }

        public virtual TColumnBuilder Title(string text)
        {
            this.Column.Title = text;
            return (this as TColumnBuilder);
        }

     }
}