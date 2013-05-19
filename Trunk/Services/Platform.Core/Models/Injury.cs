using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Injury
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String MedicalName { get; set; }

        public String OpeningStatement { get; set; }

        public String Description { get; set; }

        public ICollection<SymptomMatrixItem> SymptomMatrixItems { get; set; }

        public ICollection<InjuryWorkoutMatrixItem> InjuryWorkoutMatrixItems { get; set; }

        #endregion
    }
}
