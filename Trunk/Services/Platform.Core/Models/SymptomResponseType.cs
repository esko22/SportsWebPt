using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Core.Models
{
    public enum SymptomResponseType
    {
        Exact = 1,
        EqualAndBelowThreshold = 2,
        EqualAndAboveThreshold = 3,
        BelowThreshold = 4,
        AboveThreshold = 5,
        Any = 6,
        All = 7
    }
}
