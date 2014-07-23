using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/injuries", "GET")]
    public class InjuryListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<InjuryDto, BasicSortBy>>
    {}

    [Route("/injuries/brief", "GET")]
    public class BriefInjuryListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<BriefInjuryDto, BasicSortBy>>
    {
        #region Properties

        public int ClinicId { get; set; }

        public Boolean? IsPublic { get; set; }

        #endregion

    }


    [Route("/injuries", "POST")]
    public class CreateInjuryRequest : InjuryDto, IReturn<ApiResponse<InjuryDto>>
    { }

    [Route("/injuries/{id}", "PUT")]
    public class UpdateInjuryRequest : InjuryDto, IReturn<ApiResponse<InjuryDto>>
    { }

    [Route("/injuries/{id}", "GET")]
    public class InjuryRequest : AbstractResourceRequest, IReturn<ApiResponse<InjuryDto>>
    {}

    [Route("/injuries/{id}/publish", "PATCH")]
    public class PublishInjuryRequest : AbstractResourceRequest, IReturn<ApiResponse<InjuryDto>>
    {
        #region Properties

        public String Tags { get; set; }

        public String PageName { get; set; }

        public String OpeningStatement { get; set; }

        public Boolean Visible { get; set; }

        #endregion
    }
}
