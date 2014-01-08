using System;
using System.Collections.Generic;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class LookupService : BaseServiceStackClient, ILookupService
    {

        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public LookupService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion


        #region Methods

        public IEnumerable<SignFilter> GetSignFilters()
        {
            var request = GetSync(new SignFilterListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<SignFilter>>(request.Response.Items);
        }

        #endregion
    }
}
