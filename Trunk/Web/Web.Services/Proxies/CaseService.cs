using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class CaseService : BaseServiceStackClient, ICaseService
    {
        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public CaseService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion

        #region Methods
        
        public Case GetCase(Int64 caseId)
        {
            var request = GetSync(new CaseRequest {Id = caseId.ToString()});

            return Mapper.Map<Case>(request.Response);
        }

        public IEnumerable<Session> GetCaseSessions(Int64 caseId)
        {
            var request = GetSync(new CaseSessionListRequest { Id = caseId.ToString() });

            return Mapper.Map<IEnumerable<Session>>(request.Response.Items);
        }

        public Int64 AddCase(Case caseInstance)
        {
            var request = PostSync(Mapper.Map<CreateCaseRequest>(caseInstance));

            return request.Response.Id;
        }


        #endregion
    }

    public interface ICaseService
    {
        Int64 AddCase(Case caseInstance);
        Case GetCase(Int64 caseId);
        IEnumerable<Session> GetCaseSessions(Int64 caseId);

    }
}
