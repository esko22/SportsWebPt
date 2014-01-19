using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/treatments", "GET")]
    public class TreamentListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<TreatmentDto, BasicSortBy>>
    { }

}
