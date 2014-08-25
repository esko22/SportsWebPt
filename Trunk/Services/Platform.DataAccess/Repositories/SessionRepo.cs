using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.Repositories
{
    public class SessionRepo: EFRepository<Session>,ISessionRepo
    {
        #region Construction

        public SessionRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public IQueryable<Session> GetSessionDetails()
        {
            return GetAll()
                .Include(i => i.DifferentialDiagnosis.SymptomDetails.Select(l2 => l2.SymptomMatrixItem.BodyPartMatrixItem.BodyPart))
                .Include(i => i.DifferentialDiagnosis.SymptomDetails.Select(l2 => l2.SymptomMatrixItem.Symptom.RenderType))
                .Include(i => i.SessionPlans.Select(l2 => l2.Plan.PlanExerciseMatrixItems.Select(l3 => l3.Exercise)));
        }

        #endregion

    }

    public interface ISessionRepo : IRepository<Session>
    {
        IQueryable<Session> GetSessionDetails();
    }
}
