using System;
using System.Collections.Generic;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [Authorize]
    public class EpisodeController : ApiController
    {

        #region Fields

        private readonly IEpisodeService _episodeService;
        private readonly IUserManagementService _userManagementService;
        #endregion

        #region Construction

        public EpisodeController(IEpisodeService episodeService, IUserManagementService userManagementService)
        {
            Check.Argument.IsNotNull(episodeService, "Episode Service");
            Check.Argument.IsNotNull(userManagementService, "User Management Service");

            _episodeService = episodeService;
            _userManagementService = userManagementService;
        }

        #endregion

        #region Methods
        
        [HttpGet]
        [Route("data/episodes/{episodeId}")]
        public Episode GetEpisode(Int64 episodeId)
        {
            var episode = _episodeService.GetEpisode(episodeId);
            var user = _userManagementService.GetUserByServiceAccountId(episode.patientId);
            var therapist = _userManagementService.GetUserByServiceAccountId(episode.therapistId);
            if(user != null)
              episode.patientEmail = user.Email;
            if (therapist != null)
                episode.therapistEmail = therapist.Email;

            return episode;
        }

        [HttpGet]
        [Route("data/episodes/{episodeId}/sessions")]
        public IEnumerable<Session> GetEpisodeSessions(Int64 episodeId)
        {
            return _episodeService.GetEpisodeSessions(episodeId);
        }

        [HttpPost]
        [Route("data/episodes")]
        public Episode AddEpisode(Episode episode)
        {
            episode.therapistId = User.GetServiceAccount();
            var response = _episodeService.AddEpisode(episode);
            episode.id = response;

            return episode;
        }

        #endregion


    }
}