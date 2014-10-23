using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class User
    {
        #region Properties

        public int Id { get; set; }

        public String EmailAddress { get; set; }

        public String Hash { get; set; }

        public Boolean AccountLinked { get; set; }


        #endregion

        #region Navigation Properties

        public ICollection<Video> VideoFavorites { get; set; }
        public ICollection<Exercise> ExerciseFavorites { get; set; }
        public ICollection<Plan> PlanFavorites { get; set; }
        public ICollection<Injury> InjuryFavorites { get; set; }
        public ICollection<ClinicPatientMatrixItem> ClinicPatientMatrixItems { get; set; }
        public ICollection<Episode> Episodes { get; set; }

        public virtual ClinicAdmin ClinicAdmin { get; set; }
        public virtual Therapist Therapist { get; set; }

        #endregion

    }
}
