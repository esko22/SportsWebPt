using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlanRepo :  EFRepository<Plan>, IPlanRepo 
    {
        #region Construction

        public PlanRepo(DbContext context)
            : base(context)
        {}

        #endregion

        #region Methods

        public Plan GetFullPlanGraphById(int planId)
        {
            //TODO: investigate why I cant do both joins to equip and video in one query
            return DbSet
                .Include("PlanExerciseMatrixItems")
                .Include("PlanExerciseMatrixItems.Exercise")
                .SingleOrDefault(w => w.Id == planId);
        }


        #endregion
    }
}
