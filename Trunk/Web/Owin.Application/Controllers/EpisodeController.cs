using System;
using System.Collections.Generic;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    public class EpisodeController : ApiController
    {

        #region Fields

        private readonly IEpisodeService _episodeService;

        #endregion

        #region Construction

        public EpisodeController(IEpisodeService episodeService)
        {
            Check.Argument.IsNotNull(episodeService, "Episode Service");
            _episodeService = episodeService;
        }

        #endregion

        #region Methods
        
        [HttpGet]
        [Route("data/episodes/{episodeId}")]
        public Episode GetEpisode(Int64 episodeId)
        {
            return _episodeService.GetEpisode(episodeId);
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
            var response = _episodeService.AddEpisode(episode);
            episode.id = response;

            return episode;
        }

        #endregion


    }
}