using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class BodyPartDto
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String ScientificName { get; set; }

        public String Description { get; set; }

        public SkeletonAreaDto[] PrimaryAreas { get; set; }

        public SkeletonAreaDto[] SecondaryAreas { get; set; }

        #endregion
    }
}
