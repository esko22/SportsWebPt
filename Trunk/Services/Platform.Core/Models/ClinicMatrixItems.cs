using System;
using System.Collections;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class ClinicTherapistMatrixItem
    {

        #region Properties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int TherapistId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Therapist Therapist { get; set; }

        public virtual Clinic Clinic { get; set; }

        public ICollection<Case> Cases { get; set; }

        #endregion
    }

    public class ClinicAdminMatrixItem
    {

        #region Properties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int ClinicAdminId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ClinicAdmin Admin { get; set; }

        public virtual Clinic Clinic { get; set; }

        #endregion
    }

    public class ClinicPatientMatrixItem
    {

        #region Properties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual User Patient { get; set; }

        public virtual Clinic Clinic { get; set; }

        #endregion
    }
}
