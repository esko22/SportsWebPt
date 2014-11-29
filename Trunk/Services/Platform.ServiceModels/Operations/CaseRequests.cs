using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/cases", "GET")]
    public class CaseListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<CaseDto, BasicSortBy>>
    {
        #region Properties

        public String TherapistId { get; set; }

        public String PatientId { get; set; }

        public CaseStateDto? State { get; set; }

        #endregion
    }

    [Route("/cases/{id}/sessions", "GET")]
    public class CaseSessionListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<SessionDto, BasicSortBy>>
    {
    }


    [Route("/cases", "POST")]
    public class CreateCaseRequest : CaseDto, IReturn<ApiResponse<CaseDto>>
    {
    }

    [Route("/cases/{id}", "PUT")]
    public class UpdateCaseRequest : CaseDto, IReturn<ApiResponse<CaseDto>>
    {
    }

    [Route("/cases/{id}", "GET")]
    public class CaseRequest : AbstractResourceRequest, IReturn<ApiResponse<CaseDto>>
    { }

}
