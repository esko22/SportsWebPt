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

        public IQueryable<Plan> GetPlanDetails()
        {
            return GetAll()
                .Include(i => i.PlanCategoryMatrixItems)
                .Include(i => i.PlanExerciseMatrixItems.Select(l2 => l2.Exercise))
                .Include(i => i.PlanBodyRegionMatrixItems.Select(l2 => l2.BodyRegion))
                .Include(i => i.PublishDetail);                
        }
       
        #endregion
    }
}
