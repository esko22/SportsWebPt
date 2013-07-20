using SportsWebPt.Common.DataAccess.Ef;

namespace SportsWebPt.Platform.DataAccess.UnitOfWork
{
    public class PlanUnitOfWork : BaseUnitOfWork, IPlanUnitOfWork
    {

        #region Properties

        public IPlanRepo PlanRepo { get { return GetRepo<IPlanRepo>(); } }

        public IExerciseRepo ExerciseRepo { get { return GetRepo<IExerciseRepo>(); } } 

        #endregion

        #region Construction

        public PlanUnitOfWork(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider, new PlatformDbContext())
        {
            
        }

        #endregion
    }

    public interface IPlanUnitOfWork
    {
        #region Properties

        IPlanRepo PlanRepo { get; }

        IExerciseRepo ExerciseRepo { get; }

        #endregion
    }
}
