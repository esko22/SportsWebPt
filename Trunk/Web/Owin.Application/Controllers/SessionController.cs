using System;
using System.Net;
using System.Net.Http;
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

        [HttpGet]
        [Route("data/sessions/{sessionId}/pay")]
        public SessionPay StartSessionPay(Int64 sessionId)
        {
            return _sessionService.StartSessionPay(sessionId);
        }

        #endregion
    }

    public class PayPalController : ApiController
    {
        #region Fields

        private readonly ISessionService _sessionService;

        #endregion

        #region Construction

        public PayPalController(ISessionService sessionService)
        {
            Check.Argument.IsNotNull(sessionService, "Session Service");
            _sessionService = sessionService;
        }

        #endregion


        [HttpGet]
        [Route("data/sessions/{sessionId}/pay/execute")]
        public HttpResponseMessage ExecuteSessionPay(Int64 sessionId, String paymentId, String payerId)
        {
            var sessionPay = _sessionService.ExecuteSessionPay(sessionId, payerId, paymentId);

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = BuildSessionRedirectUri(sessionId, sessionPay.caseId);
            return response;
        }

        [HttpGet]
        [Route("data/sessions/{sessionId}/pay/cancel")]
        public HttpResponseMessage CancelSessionPay(Int64 sessionId)
        {
            var session = _sessionService.GetSession(sessionId);

            if (session == null)
                throw new Exception("Session Does Not Exist");

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = BuildSessionRedirectUri(sessionId, session.caseId);

            return response;
        }

        private Uri BuildSessionRedirectUri(Int64 sessionId, Int64 caseId)
        {
            return
                new Uri(
                    WebPlatformConfigSettings.Instance.SessionPayReturnUri.Replace("{caseId}", caseId.ToString())
                        .Replace("{sessionId}", sessionId.ToString()));
        }
    }

}