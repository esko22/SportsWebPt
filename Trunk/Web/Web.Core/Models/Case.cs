﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Web.Core
{
    public class Case
    {
        #region Properties

        public Int64 id { get; set; }

        public String state { get; set; }

        public String name { get; set; }

        public String description { get; set; }

        public DateTime createdOn { get; set; }

        public int prognosisId { get; set; }

        public int clinicPatientId { get; set; }

        public String clinicPatientIdentifier { get; set; }

        public String patientId { get; set; }

        public String therapistId { get; set; }

        public int clinicId { get; set; }

        public Prognosis prognosis { get; set; }

        public String patientEmail { get; set; }

        public String patientName { get; set; }

        public String therapistEmail { get; set; }

        public String therapistName { get; set; }

        public Clinic clinic { get; set; }

        #endregion

    }

    public class CaseSnapshot : Case
    {
        #region Properties

        public Session nextSession { get; set; }

        public Session lastSession { get; set; }

        public Plan[] lastAssignment { get; set; }


        #endregion
    }

}
