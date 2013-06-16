using System;
using System.Collections.Generic;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Application
{
    public class ExamineViewModel : BaseViewModel
    {
        #region Properties

        public IEnumerable<SymptomaticRegion> SymptomaticRegions { get; set; } 

        #endregion
    }
}