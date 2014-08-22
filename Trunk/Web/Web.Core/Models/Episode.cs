using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Web.Core
{
    public class Episode
    {
        #region Properties

        public int id { get; set; }

        public String state { get; set; }

        public String name { get; set; }

        public String description { get; set; }

        public String createdOn { get; set; }

        public int prognosisId { get; set; }

        public int clinicPatientId { get; set; }

        public int clinicTherapistId { get; set; }

        public Prognosis prognosis { get; set; }

        public String patientEmail { get; set; }

        public String therapistEmail { get; set; }

        public String clinic { get; set; }

        #endregion

    }

}
