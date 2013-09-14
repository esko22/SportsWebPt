using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class ExerciseCategoryMatrixItem
    {
        #region Properties

        public Int64 ExerciseId { get; set; }

        public FunctionCategory Category { get; set; }

        #endregion

        #region NavigationProperty

        public virtual Exercise Exercise { get; set; }

        #endregion
    }
}
