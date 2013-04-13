using System;
using System.Data.Entity;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;

namespace SportsWebPt.Platform.DataAccess
{
    public abstract class BaseUnitOfWork : IDisposable, IBaseUnitOfWork
    {
        #region
        
        protected IRepositoryProvider _repositoryProvider;
        protected DbContext _context;

        #endregion

        #region Construction

        protected BaseUnitOfWork(IRepositoryProvider repositoryProvider, DbContext context)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _repositoryProvider.DbContext = _context;

            // Do NOT enable proxied entities, else serialization fails
            _context.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            _context.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            _context.Configuration.ValidateOnSaveEnabled = false;

        }

        #endregion

        #region Methods

        public void Commit()
        {
            _context.SaveChanges();
        }

        protected IRepository<T> GetStandardRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepositoryForEntityType<T>();
        }

        protected T GetRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepository<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        #endregion

    }
}
