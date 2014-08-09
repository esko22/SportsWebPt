using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryRepo : EFRepository<Injury>, IInjuryRepo
    {
        #region Construction

        public InjuryRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public override IQueryable<Injury> GetAll()
        {
            return base.GetAll()
                .Include(i => i.PublishDetail)
                .Include(i => i.InjurySymptomMatrixItems.Select(l2 => l2.SymptomMatrixItem.Symptom))
                .Include(i => i.InjurySignMatrixItems.Select(l2 => l2.Sign.Filter))
                .Include(i => i.InjuryBodyRegionMatrixItems.Select(l2 => l2.BodyRegion));
        }

        public IQueryable<Injury> GetInjuryDetails()
        {
            return GetAll()
                .Include(i => i.InjuryPlanMatrixItems.Select(l2 => l2.Plan.PlanCategoryMatrixItems))
                .Include(i => i.InjurySignMatrixItems.Select(l2 => l2.Sign))
                .Include(i => i.InjuryCauseMatrixItems.Select(l2 => l2.Cause))
                .Include(i => i.InjuryBodyRegionMatrixItems.Select(l2 => l2.BodyRegion))
                .Include(i => i.InjurySymptomMatrixItems.Select(l2 => l2.SymptomMatrixItem).Select(l3 => l3.BodyPartMatrixItem))
                .Include(i => i.InjurySymptomMatrixItems.Select(l2 => l2.SymptomMatrixItem).Select(l3 => l3.Symptom.RenderType))
                .Include(i => i.InjuryTreatmentMatrixItems.Select(l2 => l2.Treatment))
                .Include(i => i.InjuryPrognosisMatrixItems.Select(l2 => l2.Prognosis));
        }

        #endregion
    }

    public interface IInjuryRepo : IRepository<Injury>
    {
        IQueryable<Injury> GetInjuryDetails();
    }
}
