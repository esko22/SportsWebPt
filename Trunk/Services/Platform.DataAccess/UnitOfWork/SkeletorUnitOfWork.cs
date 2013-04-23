using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SkeletonUnitOfWork : BaseUnitOfWork, ISkeletonUnitOfWork
    {
        #region Properties

        public ISkeletonRepo SkeletonAreaRepo { get { return GetRepo<ISkeletonRepo>(); } }
        public IRepository<AreaComponent> AreaComponentRepo { get { return GetStandardRepo<AreaComponent>(); } }
        public IRepository<Symptom> SymptomRepo { get { return GetStandardRepo<Symptom>(); } }
        
        #endregion

        #region Construction

        public SkeletonUnitOfWork(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider, new PlatformDbContext())
        {
        }

        #endregion
    }
}
