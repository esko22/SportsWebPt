﻿using System;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [RouteArea]
    public class LookupController : BaseController
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

        [GET("data/lookup/signfilters")]
        public ActionResult GetSignFilters()
        {
            return Json(_lookupService.GetSignFilters(), JsonRequestBehavior.AllowGet);
        }

        [GET("data/lookup/causefilters")]
        public ActionResult GetCauseFilters()
        {
            return Json(_lookupService.GetCauseFilters(), JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}
