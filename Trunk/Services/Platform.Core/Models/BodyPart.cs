using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class BodyPart
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String ScientificName { get; set; }

        public ICollection<BodyPartMatrixItem> BodyPartMatrix { get; set; }

        #endregion
    }
}
