using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class BodyPartMatrixItemDto
    {
        #region Properties

        public Int32 Id { get; set; }

        public BodyPartDto BodyPart { get; set; }

        public SkeletonAreaDto SkeletonArea { get; set; } 

        #endregion
    }
}
