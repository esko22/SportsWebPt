using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{ /// <summary>
    /// Defines a class which supports execution order.
    /// </summary>
    public abstract class OrderableTask : DisposableBase, IOrderableTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderableTask"/> class.
        /// </summary>
        [DebuggerStepThrough]
        protected OrderableTask()
        {
            Order = DefaultOrder;
        }

        /// <summary>
        /// Gets or sets the default order that the task would execute.
        /// </summary>
        /// <value>The order.</value>
        public static int DefaultOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the order that the task would execute.
        /// </summary>
        /// <value>The order.</value>
        public int Order
        {
            get;
            protected set;
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns></returns>
        public abstract BootstrapResult Execute();
    }


}
