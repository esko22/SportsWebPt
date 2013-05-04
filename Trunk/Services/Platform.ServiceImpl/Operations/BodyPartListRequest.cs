using SportsWebPt.Common.ServiceStack.Infrastructure;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class BodyPartListRequest : ApiResourceListRequest
    {
        #region Properties

        public int skeletonAreaId { get; set; }

        #endregion
    }
}
