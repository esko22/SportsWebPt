
using System;

namespace SportsWebPt.Common.Utilities
{
    public interface IDelegateReference
    {
        Delegate Target
        {
            get;
        }
    }
}