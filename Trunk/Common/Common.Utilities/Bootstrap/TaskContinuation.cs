using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Describes how the next task in the chain will be handled.
    /// </summary>
    public enum TaskContinuation
    {
        /// <summary>
        /// Executes the next task
        /// </summary>
        Continue,

        /// <summary>
        /// Skips the next task.
        /// </summary>
        Skip,

        /// <summary>
        /// Stops the execution.
        /// </summary>
        Break
    }
}
