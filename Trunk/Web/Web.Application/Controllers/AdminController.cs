using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Application;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Admin
{
    [RouteArea("Admin")]
    [AdminAccessAttribute]
    public class AdminController : BaseController
    {

        #region Fields

        private readonly IResearchService _researchService;
        private readonly IAdminService _adminService;

	    #endregion

        #region Construction

        public AdminController(IResearchService researchService, IAdminService adminService)
        {
            Check.Argument.IsNotNull(researchService, "Research Service");
            Check.Argument.IsNotNull(adminService, "Admin Service");
            
            _researchService = researchService;
            _adminService = adminService;
        }

        #endregion

        #region Methods

        [GET("data/admin/equipment", IsAbsoluteUrl = true)]
        public ActionResult GetEquipment()
        {
            var equipment = _adminService.GetEquipment();

            return Json(equipment, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/equipment", IsAbsoluteUrl = true)]
        public ActionResult AddEquipment(Equipment equipment)
        {
            var response = _adminService.AddEquipment(equipment);
            equipment.id = response;

            return Json(equipment, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/equipment/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateEquipment(Equipment equipment)
        {
            _adminService.UpdateEquipment(equipment);

            return Json(equipment, JsonRequestBehavior.DenyGet);
        }

        [GET("data/admin/videos", IsAbsoluteUrl = true)]
        public ActionResult GetVideos()
        {
            var videos = _adminService.GetVideos();

            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/videos", IsAbsoluteUrl = true)]
        public ActionResult AddVideo(Video video)
        {
            var response = _adminService.AddVideo(video);
            video.id = response;

            return Json(video, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/videos/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateVideo(Video video)
        {
            _adminService.UpdateVideo(video);

            return Json(video, JsonRequestBehavior.DenyGet);
        }

        [GET("data/admin/exercises", IsAbsoluteUrl = true)]
        public ActionResult GetExercises()
        {
            return Json(_adminService.GetClinicExercises(), JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/exercises/{id}", IsAbsoluteUrl = true)]
        public ActionResult GetExercise(Int32 id)
        {
            var exercise = _researchService.GetExercise(id);

            return Json(exercise, JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/injuries/{id}", IsAbsoluteUrl = true)]
        public ActionResult GetInjury(Int32 id)
        {
            var injury = _researchService.GetInjury(id);

            return Json(injury, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/exercises", IsAbsoluteUrl = true)]
        public ActionResult AddExercise(Exercise exercise)
        {
            var result = _adminService.AddExercise(exercise, Convert.ToInt32(User.Identity.Name));
            exercise.id = result;
            return Json(exercise, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/exercises/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateExercise(Exercise exercise)
        {
            _adminService.UpdateExercise(exercise);
            return Json(exercise, JsonRequestBehavior.DenyGet);
        }

        [GET("data/admin/bodyregions", IsAbsoluteUrl = true)]
        public ActionResult GetBodyRegions()
        {
            return Json(_researchService.GetBodyRegions(), JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/plans", IsAbsoluteUrl = true)]
        public ActionResult GetPlans()
        {
            return Json(_adminService.GetClinicPlans(), JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/plans/{id}", IsAbsoluteUrl = true)]
        public ActionResult GetPlan(Int32 id)
        {
            var plans = _researchService.GetPlan(id);

            return Json(plans, JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/bodypartmatrix", IsAbsoluteUrl = true)]
        public ActionResult GetBodyPartMatrix()
        {
            return Json(_adminService.GetBodyPartMatrix(), JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/symptoms", IsAbsoluteUrl = true)]
        public ActionResult GetSymptoms()
        {
            return Json(_adminService.GetSymtpoms(), JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/plans", IsAbsoluteUrl = true)]
        public ActionResult AddPlan(Plan plan)
        {
            var result = _adminService.AddPlan(plan, Convert.ToInt32(User.Identity.Name));
            plan.id = result;

            return Json(plan, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/plans/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdatePlan(Plan plan)
        {
            _adminService.UpdatePlan(plan);
            return Json(plan, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/plans/{Id}/publish", IsAbsoluteUrl = true)]
        public ActionResult PublishPlan(Plan plan)
        {
            _adminService.PublishPlan(plan);
            return Json(plan, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/injuries/{Id}/publish", IsAbsoluteUrl = true)]
        public ActionResult PublishInjury(Injury injury)
        {
            _adminService.PublishInjury(injury);
            return Json(injury, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/exercises/{Id}/publish", IsAbsoluteUrl = true)]
        public ActionResult PublishExercise(Exercise exercise)
        {
            _adminService.PublishExercise(exercise);
            return Json(exercise, JsonRequestBehavior.DenyGet);
        }

        [GET("data/admin/signs", IsAbsoluteUrl = true)]
        public ActionResult GetSigns()
        {
            var signs = _adminService.GetSigns();

            return Json(signs, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/signs", IsAbsoluteUrl = true)]
        public ActionResult AddSign(Sign sign)
        {
            var response = _adminService.AddSign(sign);
            sign.id = response;

            return Json(sign, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/signs/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateSign(Sign sign)
        {
            _adminService.UpdateSign(sign);

            return Json(sign, JsonRequestBehavior.DenyGet);
        }


        [GET("data/admin/causes", IsAbsoluteUrl = true)]
        public ActionResult GetCauses()
        {
            var causes = _adminService.GetCauses();

            return Json(causes, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/causes", IsAbsoluteUrl = true)]
        public ActionResult AddCause(Cause cause)
        {
            var response = _adminService.AddCause(cause);
            cause.id = response;

            return Json(cause, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/causes/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateCause(Cause cause)
        {
            _adminService.UpdateCause(cause);

            return Json(cause, JsonRequestBehavior.DenyGet);
        }

        [GET("data/admin/injuries", IsAbsoluteUrl = true)]
        public ActionResult GetInjuries()
        {
            var injuries = _adminService.GetClinicInjuries();

            return Json(injuries, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/injuries", IsAbsoluteUrl = true)]
        public ActionResult AddInjury(Injury injury)
        {
            var response = _adminService.AddInjury(injury);
            injury.id = response;

            return Json(injury, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/injuries/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateInjury(Injury injury)
        {
            _adminService.UpdateInjury(injury);

            return Json(injury, JsonRequestBehavior.DenyGet);
        }

        [GET("data/admin/bodyparts", IsAbsoluteUrl = true)]
        public ActionResult GetBodyParts()
        {
            var bodyParts = _adminService.GetBodyParts();

            return Json(bodyParts, JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/skeletonareas", IsAbsoluteUrl = true)]
        public ActionResult GetSkeletonAreas()
        {
            var skeletonAreas = _adminService.GetSkeletonAreas();

            return Json(skeletonAreas, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/bodyparts", IsAbsoluteUrl = true)]
        public ActionResult AddBodyPart(BodyPart bodyPart)
        {
            var response = _adminService.AddBodyPart(bodyPart);
            bodyPart.id = response;

            return Json(bodyPart, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/bodyparts/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateBodyPart(BodyPart bodyPart)
        {
            _adminService.UpdateBodyPart(bodyPart);

            return Json(bodyPart, JsonRequestBehavior.DenyGet);
        }

        [POST("data/admin/bodyregions", IsAbsoluteUrl = true)]
        public ActionResult AddBodyRegion(BodyRegion bodyRegion)
        {
            var response = _adminService.AddBodyRegion(bodyRegion);
            bodyRegion.id = response;

            return Json(bodyRegion, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/bodyregions/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateBodyRegion(BodyRegion bodyRegion)
        {
            _adminService.UpdateBodyRegion(bodyRegion);

            return Json(bodyRegion, JsonRequestBehavior.DenyGet);
        }

        [GET("validate/pagename", IsAbsoluteUrl = true)]
        public ActionResult ValidatePageName(string pageName)
        {
            return Json(_adminService.ValidatePageName(pageName), JsonRequestBehavior.AllowGet);
        }

        [GET("data/admin/treatments", IsAbsoluteUrl = true)]
        public ActionResult GetTreatments()
        {
            var treatments = _adminService.GetTreatments();

            return Json(treatments, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/treatments", IsAbsoluteUrl = true)]
        public ActionResult AddTreatment(Treatment treatment)
        {
            var response = _adminService.AddTreatment(treatment);
            treatment.id = response;

            return Json(treatment, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/treatments/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateTreatment(Treatment treatment)
        {
            _adminService.UpdateTreatment(treatment);

            return Json(treatment, JsonRequestBehavior.DenyGet);
        }

        [GET("data/admin/prognoses", IsAbsoluteUrl = true)]
        public ActionResult GetPrognoses()
        {
            var prognosis = _adminService.GetPrognoses();

            return Json(prognosis, JsonRequestBehavior.AllowGet);
        }

        [POST("data/admin/prognoses", IsAbsoluteUrl = true)]
        public ActionResult AddPrognosis(Prognosis prognosis)
        {
            var response = _adminService.AddPrognosis(prognosis);
            prognosis.id = response;

            return Json(prognosis, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/admin/prognoses/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdatePrognosis(Prognosis prognosis)
        {
            _adminService.UpdatePrognosis(prognosis);

            return Json(prognosis, JsonRequestBehavior.DenyGet);
        }


        #endregion


    }

    public class AdminAccessAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Check.Argument.IsNotNull(httpContext,"HttpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            var userManagementService = DependencyResolver.Current.GetService<IUserManagementService>();
            var user = userManagementService.GetUser(httpContext.User.Identity.Name);

            if(user == null)
                return false;

            return user.isAdmin && base.AuthorizeCore(httpContext);
        }
    }
}
