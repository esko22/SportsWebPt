using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class SessionDto
    {
        #region Properties

        public Int64 Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime ScheduledAt { get; set; }

        public DateTime Executed { get; set; }

        public String Notes { get; set; }

        public String ScheduledWithId { get; set; }

        public Int64 CaseId { get; set; }

        public int DifferentialDiagnosisId { get; set; }

        public SessionTypeDto SessionType { get; set; }

        public PlanDto[] Plans { get; set; }

        public DifferentialDiagnosisDto Diagnosis { get; set; }

        #endregion
    }

    public enum SessionTypeDto
    {
        Physical =1,
        Video =2,
        Audio =3
    }
}
