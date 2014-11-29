using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Core.Models
{
    public class Session
    {
        #region Properties

        public Int64 Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime ScheduledAt { get; set; }

        public DateTime Executed { get; set; }

        public String Notes { get; set; }

        public Guid ScheduledWithId { get; set; }

        public Int64 CaseId { get; set; }

        public int? DifferentialDiagnosisId { get; set; }

        public SessionType SessionType { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Therapist ScheduledWith { get; set; }

        public virtual DifferentialDiagnosis DifferentialDiagnosis { get; set; }

        public virtual Case Case { get; set; }

        public ICollection<SessionPlanMatrixItem> SessionPlans { get; set; } 

        #endregion
    }

    public class SessionPlanMatrixItem
    {
        #region Properties

        public Int64 Id { get; set; }

        public String Name { get; set; }

        public Int64 SessionId { get; set; }

        public int PlanId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Plan Plan { get; set; }

        public virtual Session Session { get; set; }

        #endregion
    }

    public enum SessionType
    {
        Physical =1,
        Video =2,
        Audio =3
    }
}
