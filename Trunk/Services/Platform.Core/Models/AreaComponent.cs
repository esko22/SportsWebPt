using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class AreaComponent
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String ScientificName { get; set; }

        public ICollection<SkeletonArea> SkeletonAreas { get; set; }

        public ICollection<Symptom> Symptoms { get; set; } 

        #endregion
    }
}
