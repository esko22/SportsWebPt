using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Core.Models
{
    public class Case
    {
        #region Properties

        public int Id { get; set; }

        public int PatientId { get; set; }

        public int ClincTherapistMatrixItemId { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public String Name { get; set; }

        #endregion

        #region Naviagation Properties

        public virtual User Patient { get; set; }

        public virtual ClinicTherapistMatrixItem ClinicTherapistMatrixItem { get; set; }

        #endregion
    }
}
