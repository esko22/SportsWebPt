using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Defines fluent interface for building a Csv File
    /// </summary>
    /// <typeparam name="T">The type of the data.</typeparam>
    public class CsvBuilder<T>  where T: class
    {
        private IColumnDataContainer<T> container;
        public CsvBuilder(IColumnDataContainer<T> container){
           Check.Argument.IsNotNull(container, "container");
           this.container = container;
        }

        public virtual CsvBuilder<T> Columns(Action<CsvColumnDataFactory<T>> addAction)
        {
            Check.Argument.IsNotNull(addAction, "addAction");
            var factory = new CsvColumnDataFactory<T>(this.container);
            addAction(factory);
            return this;
        }

       
    }
}
