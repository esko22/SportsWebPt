using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/injuries", "GET")]
    public class InjuryListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<InjuryDto, BasicSortBy>>
    {}

    [Route("/injuries", "POST")]
    public class CreateInjuryRequest : InjuryDto, IReturn<ApiResponse<InjuryDto>>
    { }

    [Route("/injuries/{id}", "PUT")]
    public class UpdateInjuryRequest : InjuryDto, IReturn<ApiResponse<InjuryDto>>
    { }

    [Route("/injuries/{id}", "GET")]
    public class InjuryRequest : AbstractResourceRequest, IReturn<ApiResponse<InjuryDto>>
    {}
}
