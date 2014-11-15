using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Episode
    {

        #region Properties

        public Int64 Id { get; set; }

        public EpisodeState State { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? PrognosisId { get; set; }

        public int ClinicPatientId { get; set; }

        public Guid TherapistId { get; set; }

        public int ClinicId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Prognosis Prognosis { get; set; }

        public virtual ClinicPatientMatrixItem ClinicPatient { get; set; }

        public virtual Therapist Therapist { get; set; }

        public virtual Clinic Clinic { get; set; }

        public ICollection<Session> Sessions { get; set; }

        #endregion
    }

    public enum EpisodeState
    {
        Pending = 1,
        Active = 2,
        InActive = 3,
        ReferredOn = 4,
        Recovered = 5
    }
}
