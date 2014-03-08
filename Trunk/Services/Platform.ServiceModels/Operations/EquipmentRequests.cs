using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/equipment", "GET")]
    public class EquipmentListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<EquipmentDto, BasicSortBy>>  
    {}

    [Route("/equipment/brief", "GET")]
    public class BriefEquipmentListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<BriefEquipmentDto, BasicSortBy>>
    { }

    [Route("/equipment", "POST")]
    public class CreateEquipmentRequest : EquipmentDto, IReturn<ApiResponse<EquipmentDto>> { }

    [Route("/equipment/{id}", "PUT")]
    public class UpdateEquipmentRequest : EquipmentDto, IReturn<ApiResponse<EquipmentDto>> { }

    [Route("/equipment/{id}", "GET")]
    public class EquipmentRequest : AbstractResourceRequest, IReturn<ApiResponse<EquipmentDto>>
    {}
}
