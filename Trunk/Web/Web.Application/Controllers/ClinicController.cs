using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttributeRouting;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [RouteArea]
    public class ClinicController : BaseController
    {
        #region Fields

        private IClinicService _clinicService;

        #endregion

        #region Construction

        public ClinicController(IClinicService clinicService)
        {
            Check.Argument.IsNotNull(clinicService, "Clinic Service");
            _clinicService = clinicService;
        }

        #endregion
    }
}