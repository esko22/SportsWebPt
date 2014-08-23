using System;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [RouteArea]
    public class EpisodeController : BaseController
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
        
        [GET("data/episodes/{episodeId}", IsAbsoluteUrl = true)]
        public ActionResult GetEpisode(Int64 episodeId)
        {
            return Json(_episodeService.GetEpisode(episodeId), JsonRequestBehavior.AllowGet);
        }

        [GET("data/episodes/{episodeId}/sessions", IsAbsoluteUrl = true)]
        public ActionResult GetEpisodeSessions(Int64 episodeId)
        {
            return Json(_episodeService.GetEpisodeSessions(episodeId), JsonRequestBehavior.AllowGet);
        }


        [POST("data/episodes", IsAbsoluteUrl = true)]
        public ActionResult AddEpisode(Episode episode)
        {
            var response = _episodeService.AddEpisode(episode);
            episode.id = response;

            return Json(episode, JsonRequestBehavior.DenyGet);
        }

        #endregion


    }
}