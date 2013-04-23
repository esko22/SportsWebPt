using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface ISkeletonRepo : IRepository<SkeletonArea>
    {
        #region Methods

        IQueryable<SkeletonArea> GetSymptonmaticRegions();


        #endregion
    }
}
