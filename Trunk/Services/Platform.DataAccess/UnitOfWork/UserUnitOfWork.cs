using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class UserUnitOfWork : BaseUnitOfWork, IUserUnitOfWork
    {
        #region Properties

        public IRepository<User> UserRepository { get { return GetStandardRepo<User>(); } }

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
    }
}
