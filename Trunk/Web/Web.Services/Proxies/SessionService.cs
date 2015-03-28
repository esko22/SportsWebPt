using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class SessionService : BaseServiceStackClient, ISessionService
    {
        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public SessionService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion

        #region Methods

        public Int64 AddSession(Session session)
        {
            var request = PostSync(Mapper.Map<CreateSessionRequest>(session));

            return request.Response.Id;
        }

        public Session UpdateSession(Session session)
        {
            var request = Put(Mapper.Map<UpdateSessionRequest>(session));

            return Mapper.Map<Session>(request.Response);
        }

        public Session GetSession(Int64 sessionId)
        {
            var request = GetSync(new SessionRequest { Id = sessionId.ToString() });

            return Mapper.Map<Session>(request.Response);
        }

        public Session GetSessionAsTherapist(Int64 sessionId, String therapistId)
        {
            var request = GetSync(new SessionRequest { Id = sessionId.ToString(), TherapistId = therapistId });

            return Mapper.Map<Session>(request.Response);
        }


        public void SetSessionPlans(Int64 sessionId, int[] planIds)
        {
            PostSync(new CreateSessionPlanRequest {Id = sessionId, PlanIds = planIds});
        }

        public SessionPay StartSessionPay(Int64 sessionId)
        {
            var request = GetSync(new StartSessionPayRequest {Id = sessionId.ToString()});

            return Mapper.Map<SessionPay>(request.Response);
        }

        public SessionPay ExecuteSessionPay(Int64 sessionId, String payerId, String paymentId)
        {
            var request = GetSync(new ExecuteSessionPayRequest { Id = sessionId.ToString(), PayerId = payerId, PaymentId = paymentId});

            return Mapper.Map<SessionPay>(request.Response);
        }


        #endregion
    }

    public interface ISessionService
    {
        Int64 AddSession(Session session);
        Session GetSession(Int64 sessionId);
        Session GetSessionAsTherapist(Int64 sessionId, String therapistId);
        void SetSessionPlans(Int64 sessionId, int[] planIds);
        Session UpdateSession(Session session);
        SessionPay StartSessionPay(Int64 sessionId);
        SessionPay ExecuteSessionPay(Int64 sessionId, String payerId, String paymentId);
    }
}
