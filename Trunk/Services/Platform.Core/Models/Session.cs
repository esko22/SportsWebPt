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

        public int ScheduledWithId { get; set; }

        public Int64 EpisodeId { get; set; }

        public int DifferentialDiagnosisId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ClinicTherapistMatrixItem ScheduledWith { get; set; }

        public virtual DifferentialDiagnosis DifferentialDiagnosis { get; set; }

        public virtual Episode Episode { get; set; }

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
        Video,
        Audio,
        Physical
    }
}
