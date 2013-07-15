using System;
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

        void UpdateExercise(Exercise exercise);
    }
}
