using System;
using System.Collections;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class ClinicTherapistMatrixItem
    {

        #region Properties

        public int Id { get; set; }

        public String ClinicTherapistIdentifier { get; set; }

        public int ClinicId { get; set; }

        public Guid TherapistId { get; set; }

        public Boolean UserConfirmed { get; set; }

        public String Pin { get; set; }

        public DateTime AddedOn { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Therapist Therapist { get; set; }

        public virtual Clinic Clinic { get; set; }
 
        #endregion
    }

    public class ClinicAdminMatrixItem
    {

        #region Properties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public Guid UserId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual User User { get; set; }

        public virtual Clinic Clinic { get; set; }

        #endregion
    }

    public class ClinicPatientMatrixItem
    {

        #region Properties

        public int Id { get; set; }

        public String ClinicPatientIdentifier { get; set; }

        public int ClinicId { get; set; }

        public Guid UserId { get; set; }

        public Boolean UserConfirmed { get; set; }

        public String Pin { get; set; }

        public DateTime AddedOn { get; set; }

        #endregion

        #region Navigation Properties

        public virtual User Patient { get; set; }

        public virtual Clinic Clinic { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        #endregion
    }

}
