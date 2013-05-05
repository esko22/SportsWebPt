using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IUserUnitOfWork : IBaseUnitOfWork
    {
        IRepository<User> UserRepository { get; }
    }
}