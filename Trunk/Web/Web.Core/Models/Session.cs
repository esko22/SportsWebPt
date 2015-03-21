using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class Session
    {
        #region Properties

        public Int64 id { get; set; }

        public string created { get; set; }

        public string scheduledStartTime { get; set; }

        public string scheduledEndTime { get; set; }

        public string videoMeetingUri { get; set; }

        public string executed { get; set; }

        public String notes { get; set; }

        public String recommendations { get; set; }

        public String healthReport { get; set; }

        public String patientDialog { get; set; }

        public String scheduledWithId { get; set; }

        public Int64 caseId { get; set; }

        public int differentialDiagnosisId { get; set; }

        public string sessionType { get; set; }

        public IEnumerable<Plan> plans { get; set; }

        public DifferentialDiagnosis diagnosis { get; set; }

        #endregion
    }
}
