using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Represents an interface which supports execution order.
    /// </summary>
    public interface IOrderableTask
    {


        /// <summary>
        /// Gets the order that the task would execute.
        /// </summary>
        /// <value>The order.</value>
        int Order
        {
            get;
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns></returns>
        BootstrapResult Execute();
    }


}
