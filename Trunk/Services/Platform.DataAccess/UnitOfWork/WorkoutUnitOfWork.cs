using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.UnitOfWork
{
    public class WorkoutUnitOfWork : BaseUnitOfWork, IWorkoutUnitOfWork
    {

        #region Properties

        public IWorkoutRepo WorkoutRepo { get { return GetRepo<IWorkoutRepo>(); } }

        public IExerciseRepo ExerciseRepo { get { return GetRepo<IExerciseRepo>(); } } 

        #endregion

        #region Construction

        public WorkoutUnitOfWork(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider, new PlatformDbContext())
        {
            
        }

        #endregion
    }

    public interface IWorkoutUnitOfWork
    {
        #region Properties

        IWorkoutRepo WorkoutRepo { get; }

        IExerciseRepo ExerciseRepo { get; }

        #endregion
    }
}
