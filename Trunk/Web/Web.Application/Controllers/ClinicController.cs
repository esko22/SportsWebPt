using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
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

        #region Methods

        [GET("data/clinics/{clinicId}", IsAbsoluteUrl = true)]
        public ActionResult GetClinic(int clinicId)
        {
            return Json(_clinicService.GetClinic(clinicId), JsonRequestBehavior.AllowGet);
        }


        [GET("data/clinics/managers/{clinicManagerId}", IsAbsoluteUrl = true)]
        public ActionResult GetManagedClinics(int clinicManagerId)
        {
            return Json(_clinicService.GetManagedClinics(clinicManagerId), JsonRequestBehavior.AllowGet);
        }

        [GET("data/clinics/{clinicId}/patients", IsAbsoluteUrl = true)]
        public ActionResult GetClinicPatients(int clinicId)
        {
            return Json(_clinicService.GetClinicPatients(clinicId), JsonRequestBehavior.AllowGet);
        }

        [GET("data/clinics/{clinicId}/therapists", IsAbsoluteUrl = true)]
        public ActionResult GetClinicTherapists(int clinicId)
        {
            return Json(_clinicService.GetClinicTherapists(clinicId), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}