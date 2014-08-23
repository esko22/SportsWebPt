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

        #endregion
    }

    public interface ISessionService
    {
        Int64 AddSession(Session session);
    }
}
