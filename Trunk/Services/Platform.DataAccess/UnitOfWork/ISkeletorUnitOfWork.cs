using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface ISkeletorUnitOfWork : IBaseUnitOfWork
    {
        IRepository<SkeletonHotspot> SkeletonHotspotRepo { get; }
    }
}
