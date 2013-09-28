using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class UserUnitOfWork : BaseUnitOfWork, IUserUnitOfWork
    {
        #region Properties

        public IRepository<User> UserRepository { get { return GetStandardRepo<User>(); } }
        public IRepository<Injury> InjuryRepository { get { return GetStandardRepo<Injury>(); } }
        public IRepository<Plan> PlanRepository { get { return GetStandardRepo<Plan>(); } }
        public IRepository<Video> VideoRepository { get { return GetStandardRepo<Video>(); } }
        public IRepository<Exercise> ExerciseRepository { get { return GetStandardRepo<Exercise>(); } }

        #endregion

        #region Construction

        public UserUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}
        
        #endregion
        
    }

    public interface IUserUnitOfWork : IBaseUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Injury> InjuryRepository { get; }
        IRepository<Plan> PlanRepository { get; }
        IRepository<Video> VideoRepository { get; }
        IRepository<Exercise> ExerciseRepository { get; }
    }
}
