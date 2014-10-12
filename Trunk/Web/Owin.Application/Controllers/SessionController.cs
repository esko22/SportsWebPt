using System;
using System.Web.Http;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    public class SessionController : ApiController
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

        [HttpPost]
        [Route("data/sessions")]
        public Session AddSession(Session session)
        {
            var response = _sessionService.AddSession(session);
            session.id = response;

            return session;
        }

        [HttpGet]
        [Route("data/sessions/{sessionId}")]
        public Session GetSession(Int64 sessionId)
        {
            return _sessionService.GetSession(sessionId);
        }

        [HttpPost]
        [Route("data/sessions/{sessionId}/plans")]
        public Boolean AddSessionPlan(Int64 sessionId, int[] planIds)
        {
            _sessionService.AddSessionPlans(sessionId,planIds);

            return true;
        }



        #endregion
    }

}