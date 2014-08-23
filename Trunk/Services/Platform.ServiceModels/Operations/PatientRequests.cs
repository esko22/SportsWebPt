using System;

using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/patients/{id}/episodes", "GET")]
    public class PatientEpisodeListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<EpisodeDto, BasicSortBy>>
    {
        #region Properties

        public EpisodeStateDto? State { get; set; }

        #endregion

    }
}
