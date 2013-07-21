﻿using System;
using System.Linq;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ResearchUnitOfWork : BaseUnitOfWork, IResearchUnitOfWork
    {
        #region Properties

        public IRepository<Equipment> EquipmentRepo { get { return GetStandardRepo<Equipment>(); } }
        public IRepository<Video> VideoRepo { get { return GetStandardRepo<Video>(); } }
        public IRepository<Exercise> ExerciseRepo { get { return GetStandardRepo<Exercise>(); } }
        public IRepository<ExerciseEquipmentMatrixItem> ExerciseEquipmentRepo { get { return GetStandardRepo<ExerciseEquipmentMatrixItem>(); } }
        public IRepository<ExerciseVideoMatrixItem> ExerciseVideoRepo { get { return GetStandardRepo<ExerciseVideoMatrixItem>(); } }
        public IRepository<ExerciseBodyRegionMatrixItem> ExerciseBodyRegionRepo { get { return GetStandardRepo<ExerciseBodyRegionMatrixItem>(); } }
        public IRepository<PlanBodyRegionMatrixItem> PlanBodyRegionRepo { get { return GetStandardRepo<PlanBodyRegionMatrixItem>(); } }
        public IRepository<PlanExerciseMatrixItem> PlanExerciseMatrixRepo { get { return GetStandardRepo<PlanExerciseMatrixItem>(); } }
        public IRepository<Sign> SignRepo { get { return GetStandardRepo<Sign>(); } }
        public IRepository<Cause> CauseRepo { get { return GetStandardRepo<Cause>(); } }
        public IRepository<Injury> InjuryRepo { get { return GetStandardRepo<Injury>(); } }
        public IPlanRepo PlanRepo { get { return GetRepo<IPlanRepo>(); } }
        public IRepository<InjuryBodyRegionMatrixItem> InjuryBodyRegionMatrixRepo { get { return GetStandardRepo<InjuryBodyRegionMatrixItem>(); } }
        public IRepository<InjuryCauseMatrixItem> InjuryCauseMatrixRepo { get { return GetStandardRepo<InjuryCauseMatrixItem>(); } }
        public IRepository<InjurySignMatrixItem> InjurySignMatrixRepo { get { return GetStandardRepo<InjurySignMatrixItem>(); } }
        public IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixRepo { get { return GetStandardRepo<InjuryPlanMatrixItem>(); } }

        #endregion

        #region Construction

        public ResearchUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public void UpdateExercise(Exercise exercise)
        {
            var execiseInDb =
                ExerciseRepo.GetAll(new[]
                    {"ExerciseEquipmentMatrixItems", "ExerciseVideoMatrixItems", "ExerciseBodyRegionMatrixItems"})
                            .SingleOrDefault(p => p.Id == exercise.Id);

            if(execiseInDb == null)
                throw new ArgumentNullException("exercise id", "exercise does not exist");

            var deletedEquipment = execiseInDb.ExerciseEquipmentMatrixItems.Except(
                exercise.ExerciseEquipmentMatrixItems, (item, matrixItem) => item.EquipmentId == matrixItem.EquipmentId).ToList();

            var addedEquipment = exercise.ExerciseEquipmentMatrixItems.Except(
                execiseInDb.ExerciseEquipmentMatrixItems, (item, matrixItem) => item.EquipmentId == matrixItem.EquipmentId).ToList();

            deletedEquipment.ForEach(e => ExerciseEquipmentRepo.Delete(e));
            addedEquipment.ForEach(e =>
            {
                e.ExerciseId = exercise.Id;
                ExerciseEquipmentRepo.Add(e);
            });

            var deletedVideos = execiseInDb.ExerciseVideoMatrixItems.Except(
                exercise.ExerciseVideoMatrixItems, (item, matrixItem) => item.VideoId == matrixItem.VideoId).ToList();

            var addedVideos = exercise.ExerciseVideoMatrixItems.Except(
                execiseInDb.ExerciseVideoMatrixItems, (item, matrixItem) => item.VideoId == matrixItem.VideoId).ToList();

            deletedVideos.ForEach(e => ExerciseVideoRepo.Delete(e));
            addedVideos.ForEach(e =>
            {
                e.ExerciseId = exercise.Id;
                ExerciseVideoRepo.Add(e);
            });

            var deletedBodyRegions = execiseInDb.ExerciseBodyRegionMatrixItems.Except(
                exercise.ExerciseBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            var addedBodyRegions = exercise.ExerciseBodyRegionMatrixItems.Except(
                execiseInDb.ExerciseBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            deletedBodyRegions.ForEach(e => ExerciseBodyRegionRepo.Delete(e));
            addedBodyRegions.ForEach(e =>
            {
                e.ExerciseId = exercise.Id;
                ExerciseBodyRegionRepo.Add(e);
            });

            var exerciseEntry = _context.Entry(execiseInDb);
            exerciseEntry.CurrentValues.SetValues(exercise);

            Commit();
        }

        public void UpdatePlan(Plan plan)
        {
            var planInDb =
                PlanRepo.GetAll(new[] { "PlanExerciseMatrixItems", "PlanBodyRegionMatrixItems" })
                            .SingleOrDefault(p => p.Id == plan.Id);

            if (planInDb == null)
                throw new ArgumentNullException("plan id", "plan does not exist");

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
        
        public void UpdateInjury(Injury injury)
        {
            var injuryInDb =
                        InjuryRepo.GetAll(new[]
                {
                    "InjuryPlanMatrixItems", "InjuryPlanMatrixItems.Plan", "InjurySignMatrixItems",
                    "InjurySignMatrixItems.Sign", "InjuryCauseMatrixItems", "InjuryCauseMatrixItems.Cause",
                    "InjuryBodyRegionMatrixItems", "InjuryBodyRegionMatrixItems.BodyRegion"
                })
                .SingleOrDefault(p => p.Id == injury.Id);

            if (injuryInDb == null)
                throw new ArgumentNullException("injury id", "injury does not exist");

            var deletedPlans = injuryInDb.InjuryPlanMatrixItems.Except(
                injury.InjuryPlanMatrixItems, (item, matrixItem) => item.PlanId == matrixItem.PlanId).ToList();

            var addedPlans = injury.InjuryPlanMatrixItems.Except(
                injuryInDb.InjuryPlanMatrixItems, (item, matrixItem) => item.PlanId == matrixItem.PlanId).ToList();

            deletedPlans.ForEach(e => InjuryPlanMatrixRepo.Delete(e));
            addedPlans.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryPlanMatrixRepo.Add(e);
            });

            var deletedSigns = injuryInDb.InjurySignMatrixItems.Except(
                injury.InjurySignMatrixItems, (item, matrixItem) => item.SignId == matrixItem.SignId).ToList();

            var addedSigns = injury.InjurySignMatrixItems.Except(
                injuryInDb.InjurySignMatrixItems, (item, matrixItem) => item.SignId == matrixItem.SignId).ToList();

            deletedSigns.ForEach(e => InjurySignMatrixRepo.Delete(e));
            addedSigns.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjurySignMatrixRepo.Add(e);
            });

            var deletedBodyRegions = injuryInDb.InjuryBodyRegionMatrixItems.Except(
                injury.InjuryBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            var addedBodyRegions = injury.InjuryBodyRegionMatrixItems.Except(
                injuryInDb.InjuryBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            deletedBodyRegions.ForEach(e => InjuryBodyRegionMatrixRepo.Delete(e));
            addedBodyRegions.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryBodyRegionMatrixRepo.Add(e);
            });

            var deletedCauses = injuryInDb.InjuryCauseMatrixItems.Except(
                injury.InjuryCauseMatrixItems, (item, matrixItem) => item.CauseId == matrixItem.CauseId).ToList();

            var addedCauses = injury.InjuryCauseMatrixItems.Except(
                injuryInDb.InjuryCauseMatrixItems, (item, matrixItem) => item.CauseId == matrixItem.CauseId).ToList();

            deletedCauses.ForEach(e => InjuryCauseMatrixRepo.Delete(e));
            addedCauses.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryCauseMatrixRepo.Add(e);
            });

            var injuryEntry = _context.Entry(injuryInDb);
            injuryEntry.CurrentValues.SetValues(injury);

            Commit();
        }

        #endregion
    }

    public interface IResearchUnitOfWork : IBaseUnitOfWork
    {
        IRepository<Equipment> EquipmentRepo { get; }
        IRepository<Video> VideoRepo { get; }
        IRepository<Exercise> ExerciseRepo { get; }
        IRepository<ExerciseEquipmentMatrixItem> ExerciseEquipmentRepo { get; }
        IRepository<ExerciseVideoMatrixItem> ExerciseVideoRepo { get; }
        IRepository<ExerciseBodyRegionMatrixItem> ExerciseBodyRegionRepo { get; }
        IRepository<PlanBodyRegionMatrixItem> PlanBodyRegionRepo { get; }
        IRepository<PlanExerciseMatrixItem> PlanExerciseMatrixRepo { get; }
        IRepository<InjuryBodyRegionMatrixItem> InjuryBodyRegionMatrixRepo { get; }
        IRepository<InjuryCauseMatrixItem> InjuryCauseMatrixRepo { get; }
        IRepository<InjurySignMatrixItem> InjurySignMatrixRepo { get; }
        IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixRepo { get; }
        IRepository<Sign> SignRepo { get; }
        IRepository<Cause> CauseRepo { get; }
        IRepository<Injury> InjuryRepo { get; } 
        IPlanRepo PlanRepo { get; } 

        void UpdateExercise(Exercise exercise);
        void UpdatePlan(Plan exercise);
        void UpdateInjury(Injury injury);
    }
}
