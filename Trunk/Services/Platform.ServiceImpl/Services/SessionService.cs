using System;
using AutoMapper;
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
            Check.Argument.IsNotNegativeOrZero(request.EpisodeId, "EpisodeId");
            Check.Argument.IsNotNegativeOrZero(request.ScheduledWithId, "Scheduled With Id");

            var session = SessionUnitOfWork.AddSession(Mapper.Map<Session>(request));

            return Ok(new ApiResponse<SessionDto>() { Response = Mapper.Map<SessionDto>(session) });
        }
            

        #endregion


    }
}
