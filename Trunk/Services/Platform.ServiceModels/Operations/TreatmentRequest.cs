using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/treatments", "GET")]
    public class TreatmentListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<TreatmentDto, BasicSortBy>>
    { }

    [Route("/treatments", "POST")]
    public class CreateTreatmentRequest : TreatmentDto, IReturn<ApiResponse<TreatmentDto>>
    {
    }

    [Route("/treatments/{id}", "PUT")]
    public class UpdateTreatmentRequest : TreatmentDto, IReturn<ApiResponse<TreatmentDto>>
    {
    }

    [Route("/treatments/{id}", "GET")]
    public class TreatmentRequest : AbstractResourceRequest, IReturn<ApiResponse<TreatmentDto>>
    { }
}
