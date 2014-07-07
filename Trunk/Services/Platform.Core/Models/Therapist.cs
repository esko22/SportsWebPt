using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Therapist
    {

        #region Properties

        public int Id { get; set; }

        public String Credentials { get; set; }

        public String Licenses { get; set; }

        #endregion


        #region Navigation Props

        public virtual User User { get; set; }

        public ICollection<ClinicTherapistMatrixItem> ClinicTherapistMatrixItems { get; set; }
        
        #endregion

    }
}
