using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class CsvColumnDataFactory<T> where T: class
    {
        private readonly IColumnDataContainer<T> container;
        private readonly IList<ColumnData<T>> list;
        public CsvColumnDataFactory(IColumnDataContainer<T> container){
            Check.Argument.IsNotNull(container, "container");
            this.container = container;
        }

        public CsvColumnDataFactory(IList<ColumnData<T>> list)
        {
            Check.Argument.IsNotNull(list, "list");
            this.list = list;
        }

        public virtual TemplateColumnDataBuilder<T> Add(Action<T> templateAction){
            Check.Argument.IsNotNull(templateAction, "templateAction");
            var item = new ColumnData<T>(templateAction);
            AddItem(item);
            return new TemplateColumnDataBuilder<T>(item);
        }

        public virtual BoundColumnDataBuilder<T> Add(Expression<Func<T, object>> expression){
            Check.Argument.IsNotNull(expression, "expression");
            var item = new ColumnData<T>(expression);
            AddItem(item);
            return new BoundColumnDataBuilder<T>(item);
        }

        protected void AddItem(ColumnData<T> item){
            if(this.list != null){
                this.list.Add(item);
            }
            else{
                this.container.Columns.Add(item);
            }
        }
    }
}
