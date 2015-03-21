using System;
using System.Web.Http;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [Authorize]
    public class SessionController : ApiController
    {

        #region Fields

        private readonly ISessionService _sessionService;

        #endregion

        #region Construction

        public SessionController(ISessionService sessionService)
        {
            Check.Argument.IsNotNull(sessionService, "Session Service");
            _sessionService = sessionService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("data/sessions")]
        public Session AddSession(Session session)
        {
            session.scheduledWithId = User.GetServiceAccount();
            var response = _sessionService.AddSession(session);
            session.id = response;

            return session;
        }

        [HttpPut]
        [Route("data/sessions/{sessionId}")]
        public Session UpdateSession(Session session)
        {
            return _sessionService.UpdateSession(session);
        }

        [HttpGet]
        [Route("data/sessions/{sessionId}")]
        public Session GetSession(Int64 sessionId)
        {
            return _sessionService.GetSession(sessionId);
        }

        [HttpGet]
        [Route("data/sessions/{sessionId}/therapist")]
        public Session GetSessionAsTherapist(Int64 sessionId)
        {
            return _sessionService.GetSessionAsTherapist(sessionId, User.GetServiceAccount());
        }

        [HttpPost]
        [Route("data/sessions/{sessionId}/plans")]
        public Boolean SetSessionPlan(Int64 sessionId, int[] planIds)
        {
            _sessionService.SetSessionPlans(sessionId,planIds);

            return true;
        }



        #endregion
    }

}