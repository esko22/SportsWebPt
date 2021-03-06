﻿using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class Session
    {
        #region Properties

        public Int64 id { get; set; }

        public DateTime created { get; set; }

        public DateTime scheduledStartTime { get; set; }

        public DateTime scheduledEndTime { get; set; }

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

        public float fee { get; set; }

        public Boolean hasPaid { get; set; }

        public String transactionId { get; set; }

        #endregion
    }

    public class SessionPay
    {
        public String payToUri { get; set; }

        public Int64 caseId { get; set; }

        public Int64 sessionId { get; set; }

    }
}
