using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels.Operations
{
    [Route("/episodes", "GET")]
    public class EpisodeListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<EpisodeDto, BasicSortBy>>
    {
        #region Properties

        public int ClinicTherapistId { get; set; }

        public int ClinicPatientId { get; set; }

        public EpisodeStateDto? State { get; set; }

        #endregion
    }

    [Route("/episodes", "POST")]
    public class CreateEpisodeRequest : EpisodeDto, IReturn<ApiResponse<EpisodeDto>>
    {
        #region Properties

        public int ClinicTherapistId { get; set; }

        public int ClinicPatientId { get; set; }

        #endregion

    }

    [Route("/episodes/{id}", "PUT")]
    public class UpdateEpisodeRequest : EpisodeDto, IReturn<ApiResponse<EpisodeDto>>
    {
    }

    [Route("/episodes/{id}", "GET")]
    public class EpisodeRequest : AbstractResourceRequest, IReturn<ApiResponse<EpisodeDto>>
    { }

}
