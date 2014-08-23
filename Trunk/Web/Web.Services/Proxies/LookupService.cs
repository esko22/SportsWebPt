using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Filter> GetSignFilters()
        {
            var request = GetSync(new FilterListRequest() { FilterType = FilterTypeDto.Sign } );

            return request.Response == null ? null : Mapper.Map<IEnumerable<Filter>>(request.Response.Items);
        }

        public IEnumerable<Filter> GetCauseFilters()
        {
            var request = GetSync(new FilterListRequest() { FilterType = FilterTypeDto.Cause });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Filter>>(request.Response.Items);
        }

        #endregion
    }

    public interface ILookupService
    {
        #region Methods

        IEnumerable<Filter> GetSignFilters();

        IEnumerable<Filter> GetCauseFilters();

        #endregion
    }
}
