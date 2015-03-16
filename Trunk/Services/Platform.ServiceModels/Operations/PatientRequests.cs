using System;

using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/patients/{id}/cases", "GET")]
    public class PatientCaseListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<CaseDto, BasicSortBy>>
    {
        #region Properties

        public CaseStateDto? State { get; set; }

        #endregion

    }

    [Route("/patients/{id}/snapshot", "GET")]
    public class PatientSnapshotRequest : AbstractResourceRequest, IReturn<ApiResponse<PatientSnapshotDto>>
    {
    }
}
