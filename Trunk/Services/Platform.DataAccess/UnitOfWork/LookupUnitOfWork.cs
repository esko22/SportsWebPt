using System;
using System.Collections.Generic;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class LookupUnitOfWork : BaseUnitOfWork, ILookupUnitOfWork
    {
        #region Fields

        public IRepository<SignFilter> SignFilterRepo  { get { return GetStandardRepo<SignFilter>(); } }  

        #endregion

        #region Construction 

        public LookupUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public IEnumerable<SignFilter> GetSignFilters()
        {
            return SignFilterRepo.GetAll();
        } 

        #endregion
    }

    public interface ILookupUnitOfWork
    {
        IEnumerable<SignFilter> GetSignFilters();
    }
}
