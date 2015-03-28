using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class SessionDto
    {
        #region Properties

        public Int64 Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime ScheduledStartTime { get; set; }

        public DateTime ScheduledEndTime { get; set; }

        public String VideoMeetingUri { get; set; }

        public DateTime? Executed { get; set; }

        public String Notes { get; set; }

        public String Recommendations { get; set; }

        public String HealthReport { get; set; }

        public String PatientDialog { get; set; }

        public String ScheduledWithId { get; set; }

        public Int64 CaseId { get; set; }

        public int DifferentialDiagnosisId { get; set; }

        public SessionTypeDto SessionType { get; set; }

        public PlanDto[] Plans { get; set; }

        public DifferentialDiagnosisDto Diagnosis { get; set; }

        public float Fee { get; set; }

        public Boolean HasPaid { get; set; }

        public String TransactionId { get; set; }

        #endregion
    }

    public enum SessionTypeDto
    {
        Physical =1,
        Video =2,
        Audio =3,
        Email = 4
    }

    public class SessionPayDto
    {
        public String PayToUri { get; set; }

        public Int64 CaseId { get; set; }

        public Int64 SessionId { get; set; }

    }
}
