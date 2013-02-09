using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    [Flags]
    public enum ImplicitUseKindFlags
    {
        Default = Access | Assign | Instantiated,

        /// <summary>
        /// Only entity marked with attribute considered used
        /// </summary>
        Access = 1,

        /// <summary>
        /// Indicates implicit assignment to a member
        /// </summary>
        Assign = 2,

        /// <summary>
        /// Indicates implicit instantiation of a type
        /// </summary>
        Instantiated = 4,
    }
}
