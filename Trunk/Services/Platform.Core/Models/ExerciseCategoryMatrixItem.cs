using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class ExerciseCategoryMatrixItem
    {
        #region Properties

        public int ExerciseId { get; set; }

        public virtual FunctionCategory Category { get; set; }

        #endregion

        #region NavigationProperty

        public virtual Exercise Exercise { get; set; }

        #endregion
    }
}
