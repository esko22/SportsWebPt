using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface ISkeletonUnitOfWork : IBaseUnitOfWork
    {
        IRepository<SkeletonHotspot> SkeletonHotspotRepo { get; }
    }
}
