using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [RouteArea]
    public class SessionController : BaseController
    {

        #region Fields

        private readonly ISessionService _sessionService;

        #endregion

        #region Construction

        public SessionController(ISessionService sessionService)
        {
            Check.Argument.IsNotNull(sessionService, "Episode Service");
            _sessionService = sessionService;
        }

        #endregion

        #region Methods

        [POST("data/sessions", IsAbsoluteUrl = true)]
        public ActionResult AddSession(Session session)
        {
            var response = _sessionService.AddSession(session);
            session.id = response;

            return Json(session, JsonRequestBehavior.DenyGet);
        }

        [GET("data/sessions/{sessionId}", IsAbsoluteUrl = true)]
        public ActionResult GetSession(Int64 sessionId)
        {
            return Json(_sessionService.GetSession(sessionId), JsonRequestBehavior.AllowGet);
        }

        [POST("data/sessions/{sessionId}/plans", IsAbsoluteUrl = true)]
        public ActionResult AddSessionPlan(Int64 sessionId, int[] planIds)
        {
            _sessionService.AddSessionPlans(sessionId,planIds);
          
            return Json(null, JsonRequestBehavior.DenyGet);
        }



        #endregion
    }

}