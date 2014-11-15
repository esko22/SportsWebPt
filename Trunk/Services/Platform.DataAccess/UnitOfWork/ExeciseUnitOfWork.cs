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
    public class ExeciseUnitOfWork : BaseUnitOfWork, IExerciseUnitOfWork
    {

        #region Properties

        public IRepository<ClinicExerciseMatrixItem> ClinicExerciseRepo { get { return GetStandardRepo<ClinicExerciseMatrixItem>(); } }
        public IExerciseRepo ExerciseRepo { get { return GetRepo<IExerciseRepo>(); } }
        public IRepository<ExercisePublishDetail> ExercisePublishDetailRepo { get { return GetStandardRepo<ExercisePublishDetail>(); } }

        #endregion
        
        #region Fields

        private IRepository<ExerciseEquipmentMatrixItem> ExerciseEquipmentRepo { get { return GetStandardRepo<ExerciseEquipmentMatrixItem>(); } }
        private IRepository<ExerciseVideoMatrixItem> ExerciseVideoRepo { get { return GetStandardRepo<ExerciseVideoMatrixItem>(); } }
        private IRepository<ExerciseBodyRegionMatrixItem> ExerciseBodyRegionRepo { get { return GetStandardRepo<ExerciseBodyRegionMatrixItem>(); } }
        private IRepository<ExerciseBodyPartMatrixItem> ExerciseBodyPartRepo { get { return GetStandardRepo<ExerciseBodyPartMatrixItem>(); } }
        private IRepository<ExerciseCategoryMatrixItem> ExerciseCategoryRepo { get { return GetStandardRepo<ExerciseCategoryMatrixItem>(); } }

        #endregion

        #region Construction

        public ExeciseUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public void UpdateExercise(Exercise exercise)
        {
            var execiseInDb =
                ExerciseRepo.GetExerciseDetailForUpdate()
                            .SingleOrDefault(p => p.Id == exercise.Id);

            if (execiseInDb == null)
                throw new ArgumentNullException("exercise id", "exercise does not exist");

            var deletedCategories = execiseInDb.ExerciseCategoryMatrixItems.Except(
                exercise.ExerciseCategoryMatrixItems, (item, matrixItem) => item.Category == matrixItem.Category).ToList();

            var addedCategories = exercise.ExerciseCategoryMatrixItems.Except(
                execiseInDb.ExerciseCategoryMatrixItems, (item, matrixItem) => item.Category == matrixItem.Category).ToList();

            deletedCategories.ForEach(e => ExerciseCategoryRepo.Delete(e));
            addedCategories.ForEach(e =>
            {
                e.ExerciseId = exercise.Id;
                ExerciseCategoryRepo.Add(e);
            });

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

            var deletedBodyParts = execiseInDb.ExerciseBodyPartMatrixItems.Except(
                exercise.ExerciseBodyPartMatrixItems, (item, matrixItem) => item.BodyPartId == matrixItem.BodyPartId).ToList();

            var addedBodyParts = exercise.ExerciseBodyPartMatrixItems.Except(
                execiseInDb.ExerciseBodyPartMatrixItems, (item, matrixItem) => item.BodyPartId == matrixItem.BodyPartId).ToList();

            deletedBodyParts.ForEach(e => ExerciseBodyPartRepo.Delete(e));
            addedBodyParts.ForEach(e =>
            {
                e.ExerciseId = exercise.Id;
                ExerciseBodyPartRepo.Add(e);
            });

            var exerciseEntry = _context.Entry(execiseInDb);
            exerciseEntry.CurrentValues.SetValues(exercise);

            Commit();
        }

        public IQueryable<Exercise> GetSharedExercisesByTherapist(Guid therapistId)
        {
            return ExerciseRepo.GetAll()
                .Include(i => i.ClinicExerciseMatrixItems.Select(l2 => l2.Clinic))
                .Where(p => p.TherapistExerciseMatrixItems.Any(a => a.TherapistId == therapistId && a.IsOwner))
                .OrderBy(p => p.Id);
        }

        public void UpdateSharedExercises(IEnumerable<ClinicExerciseMatrixItem> sharedExercises)
        {
            foreach (var clinicExerciseMatrixItem in sharedExercises)
            {
                if (clinicExerciseMatrixItem.Id > 0)
                {
                    var clinicExercise = ClinicExerciseRepo.GetById(clinicExerciseMatrixItem.Id);
                    clinicExercise.IsActive = clinicExerciseMatrixItem.IsActive;
                }
                else
                    ClinicExerciseRepo.Add(clinicExerciseMatrixItem);
            }

            Commit();
        }

        #endregion
    }

    public interface IExerciseUnitOfWork : IBaseUnitOfWork
    {
        IExerciseRepo ExerciseRepo { get; }
        IRepository<ExercisePublishDetail> ExercisePublishDetailRepo { get; }

        void UpdateExercise(Exercise exercise);
        IQueryable<Exercise> GetSharedExercisesByTherapist(Guid therapistId);
        void UpdateSharedExercises(IEnumerable<ClinicExerciseMatrixItem> sharedExercises);
    }
}
