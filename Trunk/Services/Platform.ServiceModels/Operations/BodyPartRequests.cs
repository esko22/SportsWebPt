using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/bodyparts", "GET")]
    public class BodyPartListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<BodyPartDto, BodyPartSortBy>>
    {
        #region Properties

        public int SkeletonAreaId { get; set; }

        #endregion
    }

    [Route("/bodyparts", "POST")]
    public class CreateBodyPartRequest : BodyPartDto, IReturn<ApiResponse<BodyPartDto>>
    {
    }


    [Route("/bodyparts/{id}", "GET")]
    public class BodyPartRequest : AbstractResourceRequest, IReturn<ApiResponse<BodyPartDto>>
    {
    }

    [Route("/bodyparts/{id}", "PUT")]
    public class UpdateBodyPartRequest : BodyPartDto, IReturn<ApiResponse<BodyPartDto>>
    {
    }

    [Route("/bodypartmatrix", "GET")]
    public class BodyPartMatrixListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<BodyPartMatrixItemDto, BasicSortBy>>
    {
    }

}
