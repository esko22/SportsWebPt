using System;
using System.Collections.Generic;
using System.Data.Entity;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlatformRepositoryFactory : RepositoryFactory
    {
        protected override IDictionary<Type, Func<DbContext, object>> GetNonStandardRespositories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                {
                   {typeof(IUserRepository), dbContext => new UserRepo(dbContext)},
                   {typeof(IRepository<SkeletonHotspot>), dbContext => new SkeletonRepo(dbContext)}
                };
        }

    }
}
