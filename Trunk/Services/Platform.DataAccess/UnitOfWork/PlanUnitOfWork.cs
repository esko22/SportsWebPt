using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Common.Utilities;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlanUnitOfWork : BaseUnitOfWork, IPlanUnitOfWork
    {
        #region Properties

        public IRepository<ClinicPlanMatrixItem> ClinicPlanRepo { get { return GetStandardRepo<ClinicPlanMatrixItem>(); } }
        public IPlanRepo PlanRepo { get { return GetRepo<IPlanRepo>(); } }
        public IExerciseRepo ExerciseRepo { get { return GetRepo<IExerciseRepo>(); } } 
        public IRepository<PlanBodyRegionMatrixItem> PlanBodyRegionRepo { get { return GetStandardRepo<PlanBodyRegionMatrixItem>(); } }
        public IRepository<PlanExerciseMatrixItem> PlanExerciseMatrixRepo { get { return GetStandardRepo<PlanExerciseMatrixItem>(); } }
        public IRepository<PlanCategoryMatrixItem> PlanCategoryRepo { get { return GetStandardRepo<PlanCategoryMatrixItem>(); } }

        #endregion

        #region Construction

        public PlanUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public void UpdatePlan(Plan plan)
        {
            var planInDb = PlanRepo.GetPlanDetails()
                            .SingleOrDefault(p => p.Id == plan.Id);

            if (planInDb == null)
                throw new ArgumentNullException("plan id", "plan does not exist");

            var deletedCategories = planInDb.PlanCategoryMatrixItems.Except(
                plan.PlanCategoryMatrixItems, (item, matrixItem) => item.Category == matrixItem.Category).ToList();

            var planCategories = plan.PlanCategoryMatrixItems.Except(
                planInDb.PlanCategoryMatrixItems, (item, matrixItem) => item.Category == matrixItem.Category).ToList();

            deletedCategories.ForEach(e => PlanCategoryRepo.Delete(e));
            planCategories.ForEach(e =>
            {
                e.PlanId = plan.Id;
                PlanCategoryRepo.Add(e);
            });

            var deletedExercises = planInDb.PlanExerciseMatrixItems.Except(
                plan.PlanExerciseMatrixItems, (item, matrixItem) => item.ExerciseId == matrixItem.ExerciseId).ToList();

            var addedExercises = plan.PlanExerciseMatrixItems.Except(
                planInDb.PlanExerciseMatrixItems, (item, matrixItem) => item.ExerciseId == matrixItem.ExerciseId).ToList();

            deletedExercises.ForEach(e => PlanExerciseMatrixRepo.Delete(e));
            addedExercises.ForEach(e =>
            {
                e.PlanId = plan.Id;
                PlanExerciseMatrixRepo.Add(e);
            });

            var deletedBodyRegions = planInDb.PlanBodyRegionMatrixItems.Except(
                plan.PlanBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            var addedBodyRegions = plan.PlanBodyRegionMatrixItems.Except(
                planInDb.PlanBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            deletedBodyRegions.ForEach(e => PlanBodyRegionRepo.Delete(e));
            addedBodyRegions.ForEach(e =>
            {
                e.PlanId = plan.Id;
                PlanBodyRegionRepo.Add(e);
            });

            var planEntry = _context.Entry(planInDb);
            planEntry.CurrentValues.SetValues(plan);

            Commit();
        }


        #endregion

    }

    public interface IPlanUnitOfWork : IBaseUnitOfWork
    {

        #region Properties

        IRepository<ClinicPlanMatrixItem> ClinicPlanRepo { get; }
        IPlanRepo PlanRepo { get; }
        IRepository<PlanBodyRegionMatrixItem> PlanBodyRegionRepo { get; }
        IRepository<PlanExerciseMatrixItem> PlanExerciseMatrixRepo { get; }
        IRepository<PlanCategoryMatrixItem> PlanCategoryRepo { get; }
        IExerciseRepo ExerciseRepo { get; } 

        #endregion

        #region Methods

        void UpdatePlan(Plan exercise);


        #endregion
    }

}
