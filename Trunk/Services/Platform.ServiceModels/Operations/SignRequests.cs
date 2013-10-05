using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/signs", "GET")]
    public class SignListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<SignDto, BasicSortBy>> 
    {}

    [Route("/signs", "POST")]
    public class CreateSignRequest : SignDto, IReturn<ApiResponse<SignDto>>
    {}

    [Route("/signs/{id}", "PUT")]
    public class UpdateSignRequest : SignDto, IReturn<ApiResponse<SignDto>>
    {}

    [Route("/signs/{id}", "GET")]
    public class SignRequest : AbstractResourceRequest, IReturn<ApiResponse<SignDto>>
    {}
}
