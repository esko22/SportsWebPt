using System;
using System.Collections.Generic;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    public class LookupController : ApiController
    {
        #region Fields

        private ILookupService _lookupService;

        #endregion

        #region Construction

        public LookupController(ILookupService lookupService)
        {
            Check.Argument.IsNotNull(lookupService,"lookupService");
            _lookupService = lookupService;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("data/lookup/signfilters")]
        public IEnumerable<Filter> GetSignFilters()
        {
            return _lookupService.GetSignFilters();
        }

        [HttpGet]
        [Route("data/lookup/causefilters")]
        public IEnumerable<Filter> GetCauseFilters()
        {
            return _lookupService.GetCauseFilters();
        }

        #endregion


    }
}
