using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SkeletonUnitOfWork : BaseUnitOfWork, ISkeletonUnitOfWork
    {
        #region Properties

        public IRepository<SkeletonArea> SkeletonAreaRepo { get { return GetRepo<IRepository<SkeletonArea>>(); } }
        public IRepository<AreaComponent> AreaComponentRepo { get { return GetStandardRepo<AreaComponent>(); } } 

        #endregion

        #region Construction

        public SkeletonUnitOfWork(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider, new PlatformDbContext())
        {
        }

        #endregion
    }
}
