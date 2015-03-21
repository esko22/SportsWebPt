using System;
using System.Linq;
using AutoMapper;
using ServiceStack.Redis;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SessionService : RestService
    {
        #region Properties

        public ISessionUnitOfWork SessionUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Post(CreateSessionRequest request)
        {
            Check.Argument.IsNotNull(request, "Session Cannot Be Null");
            Check.Argument.IsNotNegativeOrZero(request.CaseId, "CaseId");
            Check.Argument.IsNotEmpty(request.ScheduledWithId, "Scheduled With Id");

            var session = SessionUnitOfWork.AddSession(Mapper.Map<Session>(request));

            return Ok(new ApiResponse<SessionDto>() { Response = Mapper.Map<SessionDto>(session) });
        }

        public object Post(CreateSessionPlanRequest request)
        {
            Check.Argument.IsNotNull(request, "Session Cannot Be Null");
            Check.Argument.IsNotEmpty(request.PlanIds, "Plan Ids");

            SessionUnitOfWork.SetSessionPlans(request.Id, request.PlanIds);

            return Ok();
        }


        public object Get(SessionRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsLong, "SessionId");

            var session =
                SessionUnitOfWork.SessionRepo.GetSessionDetails().SingleOrDefault(p => p.Id == request.IdAsLong);

            if (session == null)
                NotFound("Session Not Found");

            return Ok(new ApiResponse<SessionDto>() { Response = Mapper.Map<SessionDto>(session) });
        }

        public object Put(UpdateSessionRequest request)
        {
            Check.Argument.IsNotNull(request, "SessionDto");

            SessionUnitOfWork.UpdateSession(Mapper.Map<Session>(request));

            return Ok(new ApiResponse<SessionDto>(request));
        }
            

        #endregion


    }
}
