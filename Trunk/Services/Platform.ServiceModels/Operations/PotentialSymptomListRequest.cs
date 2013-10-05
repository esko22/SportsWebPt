using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/potentialsymptoms","GET")]
    [Route("/potentialsymptoms/{BodyPartMatrixId}", "GET")]
    public class PotentialSymptomListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<PotentialSymptomDto, BasicSortBy>>    
    {
        #region Properties

        public int BodyPartMatrixId { get; set; }

        #endregion
    }
}
