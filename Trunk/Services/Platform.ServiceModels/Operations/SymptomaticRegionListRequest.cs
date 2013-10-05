using System;
using ServiceStack.ServiceHost;

using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/symptomaticregions", "GET")]
    public class SymptomaticRegionListRequest : IReturn<ApiListResponse<SymptomaticRegionDto, BasicSortBy>>
    {}

    [Route("/symptoms", "GET")]
    public class SymptomListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<SymptomDto, BasicSortBy>>
    {}

}
