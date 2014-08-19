using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Episode
    {

        #region Properties

        public Int64 Id { get; set; }

        public EpisodeState State { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PrognosisId { get; set; }

        public int ClinicPatientId { get; set; }

        public int ClinicTherapistId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Prognosis Prognosis { get; set; }

        public virtual ClinicPatientMatrixItem ClinicPatient { get; set; }

        public virtual ClinicTherapistMatrixItem ClinicTherapist { get; set; }

        public ICollection<Session> Sessions { get; set; }

        #endregion
    }

    public enum EpisodeState
    {
        Pending,
        Active,
        InActive,
        ReferredOn,
        Recovered
    }
}
