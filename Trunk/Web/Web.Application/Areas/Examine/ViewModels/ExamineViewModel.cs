using System;
using System.Collections.Generic;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Application
{
    public class ExamineViewModel : BaseViewModel
    {
        #region Properties

        public IEnumerable<SkeletonHotspot> SkeletonHotspots { get; set; } 

        #endregion
    }
}