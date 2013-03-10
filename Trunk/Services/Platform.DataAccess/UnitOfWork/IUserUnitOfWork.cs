namespace SportsWebPt.Platform.DataAccess
{
    public interface IUserUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}