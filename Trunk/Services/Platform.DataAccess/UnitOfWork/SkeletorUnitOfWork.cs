using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SkeletorUnitOfWork : BaseUnitOfWork, ISkeletorUnitOfWork
    {
        #region Properties

        public IRepository<SkeletonHotspot> SkeletonHotspotRepo { get { return GetRepo<SkeletorRepo>(); } }

        #endregion

        #region Construction

        public SkeletorUnitOfWork(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider, new PlatformDbContext())
        { }

        #endregion
    }
}
