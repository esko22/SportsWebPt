using System.Linq;
using System.Collections.Generic;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class LookupUnitOfWork : BaseUnitOfWork, ILookupUnitOfWork
    {
        #region Fields

        public IRepository<Filter> FilterRepo  { get { return GetStandardRepo<Filter>(); } }  

        #endregion

        #region Construction 

        public LookupUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public IEnumerable<Filter> GetSignFilters()
        {
            return FilterRepo.GetAll().Where(p => p.FilterType == FilterType.Sign);
        }

        public IEnumerable<Filter> GetCauseFilters()
        {
            return FilterRepo.GetAll().Where(p => p.FilterType == FilterType.Cause);
        }

        public IEnumerable<Filter> GetFilters()
        {
            return FilterRepo.GetAll();
        }

        #endregion
    }

    public interface ILookupUnitOfWork
    {
        IEnumerable<Filter> GetSignFilters();
        IEnumerable<Filter> GetCauseFilters();
        IEnumerable<Filter> GetFilters();
    }
}
