using SportsWebPt.Common.ServiceStack.Infrastructure;

namespace SportsWebPt.Platform.ServiceModels
{
    public class BodyPartListRequest : ApiResourceListRequest
    {
        #region Properties

        public int skeletonAreaId { get; set; }

        #endregion
    }

    public class BodyPartRequest : ApiResourceRequest<BodyPartDto>
    {
    }

    public class BodyPartMatrixListRequest : ApiResourceListRequest
    {
    }

}
