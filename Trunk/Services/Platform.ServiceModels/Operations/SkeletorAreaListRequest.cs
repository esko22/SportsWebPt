using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/areas", "GET")]
    public class SkeletonAreaListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<SkeletonAreaDto, SkeletonSortBy>>
    {}
}
