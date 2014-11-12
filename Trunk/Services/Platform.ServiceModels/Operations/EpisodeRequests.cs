using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/episodes", "GET")]
    public class EpisodeListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<EpisodeDto, BasicSortBy>>
    {
        #region Properties

        public String ClinicTherapistId { get; set; }

        public String ClinicPatientId { get; set; }

        public EpisodeStateDto? State { get; set; }

        #endregion
    }

    [Route("/episodes/{id}/sessions", "GET")]
    public class EpisodeSessionListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<SessionDto, BasicSortBy>>
    {
    }


    [Route("/episodes", "POST")]
    public class CreateEpisodeRequest : EpisodeDto, IReturn<ApiResponse<EpisodeDto>>
    {
    }

    [Route("/episodes/{id}", "PUT")]
    public class UpdateEpisodeRequest : EpisodeDto, IReturn<ApiResponse<EpisodeDto>>
    {
    }

    [Route("/episodes/{id}", "GET")]
    public class EpisodeRequest : AbstractResourceRequest, IReturn<ApiResponse<EpisodeDto>>
    { }

}
