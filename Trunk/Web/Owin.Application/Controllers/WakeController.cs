using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    public class WakeController : ApiController
    {
        #region Fields

        private ILookupService _lookupService;

        #endregion

        #region Construction

        public WakeController(ILookupService lookupService)
        {
            Check.Argument.IsNotNull(lookupService, "lookupService");
            _lookupService = lookupService;
        }

        #endregion

        [HttpGet]
        [Route("wake")]
        public dynamic WakeRoutine()
        {
            _lookupService.GetSignFilters();
            UserManagementService.UserAccountServiceFactory().GetByEmail("test@me.com");

            return "I am up!!";
        }
    }
}