using System.Collections.Generic;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IExamineService
    {
        IEnumerable<SkeletonArea> GetSkeletonAreas();

        IEnumerable<AreaComponent> GetAreaComponents(int skeletionAreaId);
    }
}
