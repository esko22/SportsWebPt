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
    public class IndexController : BaseController
    {

        #region Fields

        private readonly IResearchService _researchService;
        private readonly IAdminService _adminService;

	    #endregion

        #region Construction

        public IndexController(IResearchService researchService, IAdminService adminService)
        {
            Check.Argument.IsNotNull(researchService, "Research Service");
            Check.Argument.IsNotNull(adminService, "Admin Service");
            
            _researchService = researchService;
            _adminService = adminService;
        }

        #endregion

        #region Methods
        
        [GET("Admin", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            var viewModel = CreateViewModel<AdminIndexViewModel>();

            return View(viewModel);
        }

        [GET("admin/equipment", IsAbsoluteUrl = true)]
        public ActionResult GetEquipment()
        {
            var equipment = _adminService.GetEquipment();

            return Json(equipment, JsonRequestBehavior.AllowGet);
        }

        [POST("admin/equipment", IsAbsoluteUrl = true)]
        public ActionResult AddEquipment(Equipment equipment)
        {
            var response = _adminService.AddEquipment(equipment);
            equipment.id = response;

            return Json(equipment, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/equipment/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateEquipment(Equipment equipment)
        {
            _adminService.UpdateEquipment(equipment);

            return Json(equipment, JsonRequestBehavior.DenyGet);
        }

        [GET("admin/videos", IsAbsoluteUrl = true)]
        public ActionResult GetVideos()
        {
            var videos = _adminService.GetVideos();

            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        [POST("admin/videos", IsAbsoluteUrl = true)]
        public ActionResult AddVideo(Video video)
        {
            var response = _adminService.AddVideo(video);
            video.id = response;

            return Json(video, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/videos/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateVideo(Video video)
        {
            _adminService.UpdateVideo(video);

            return Json(video, JsonRequestBehavior.DenyGet);
        }

        [GET("admin/exercises", IsAbsoluteUrl = true)]
        public ActionResult GetExercises()
        {
            return Json(_adminService.GetExercises(), JsonRequestBehavior.AllowGet);
        }


        [POST("admin/exercises", IsAbsoluteUrl = true)]
        public ActionResult AddExercise(Exercise exercise)
        {
            var result = _adminService.AddExercise(exercise);
            exercise.id = result;
            return Json(exercise, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/exercises/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateExercise(Exercise exercise)
        {
            _adminService.UpdateExercise(exercise);
            return Json(exercise, JsonRequestBehavior.DenyGet);
        }

        [GET("admin/bodyregions", IsAbsoluteUrl = true)]
        public ActionResult GetBodyRegions()
        {
            return Json(_researchService.GetBodyRegions(), JsonRequestBehavior.AllowGet);
        }

        [GET("admin/plans", IsAbsoluteUrl = true)]
        public ActionResult GetPlans()
        {
            return Json(_adminService.GetPlans(), JsonRequestBehavior.AllowGet);
        }

        [GET("admin/bodypartmatrix", IsAbsoluteUrl = true)]
        public ActionResult GetBodyPartMatrix()
        {
            return Json(_adminService.GetBodyPartMatrix(), JsonRequestBehavior.AllowGet);
        }

        [GET("admin/symptoms", IsAbsoluteUrl = true)]
        public ActionResult GetSymptoms()
        {
            return Json(_adminService.GetSymtpoms(), JsonRequestBehavior.AllowGet);
        }

        [POST("admin/plans", IsAbsoluteUrl = true)]
        public ActionResult AddPlan(Plan plan)
        {
            var result = _adminService.AddPlan(plan);
            plan.id = result;

            return Json(plan, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/plans/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdatePlan(Plan plan)
        {
            _adminService.UpdatePlan(plan);
            return Json(plan, JsonRequestBehavior.DenyGet);
        }

        [GET("admin/signs", IsAbsoluteUrl = true)]
        public ActionResult GetSigns()
        {
            var signs = _adminService.GetSigns();

            return Json(signs, JsonRequestBehavior.AllowGet);
        }

        [POST("admin/signs", IsAbsoluteUrl = true)]
        public ActionResult AddSign(Sign sign)
        {
            var response = _adminService.AddSign(sign);
            sign.id = response;

            return Json(sign, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/signs/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateSign(Sign sign)
        {
            _adminService.UpdateSign(sign);

            return Json(sign, JsonRequestBehavior.DenyGet);
        }


        [GET("admin/causes", IsAbsoluteUrl = true)]
        public ActionResult GetCauses()
        {
            var causes = _adminService.GetCauses();

            return Json(causes, JsonRequestBehavior.AllowGet);
        }

        [POST("admin/causes", IsAbsoluteUrl = true)]
        public ActionResult AddCause(Cause cause)
        {
            var response = _adminService.AddCause(cause);
            cause.id = response;

            return Json(cause, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/causes/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateCause(Cause cause)
        {
            _adminService.UpdateCause(cause);

            return Json(cause, JsonRequestBehavior.DenyGet);
        }

        [GET("admin/injuries", IsAbsoluteUrl = true)]
        public ActionResult GetInjuries()
        {
            var injuries = _adminService.GetInjuries();

            return Json(injuries, JsonRequestBehavior.AllowGet);
        }

        [POST("admin/injuries", IsAbsoluteUrl = true)]
        public ActionResult AddInjury(Injury injury)
        {
            var response = _adminService.AddInjury(injury);
            injury.id = response;

            return Json(injury, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/injuries/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateInjury(Injury injury)
        {
            _adminService.UpdateInjury(injury);

            return Json(injury, JsonRequestBehavior.DenyGet);
        }

        [GET("admin/bodyparts", IsAbsoluteUrl = true)]
        public ActionResult GetBodyParts()
        {
            var bodyParts = _adminService.GetBodyParts();

            return Json(bodyParts, JsonRequestBehavior.AllowGet);
        }

        [GET("admin/skeletonareas", IsAbsoluteUrl = true)]
        public ActionResult GetSkeletonAreas()
        {
            var skeletonAreas = _adminService.GetSkeletonAreas();

            return Json(skeletonAreas, JsonRequestBehavior.AllowGet);
        }

        [POST("admin/bodyparts", IsAbsoluteUrl = true)]
        public ActionResult AddBodyPart(BodyPart bodyPart)
        {
            var response = _adminService.AddBodyPart(bodyPart);
            bodyPart.id = response;

            return Json(bodyPart, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/bodyparts/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateBodyPart(BodyPart bodyPart)
        {
            _adminService.UpdateBodyPart(bodyPart);

            return Json(bodyPart, JsonRequestBehavior.DenyGet);
        }

        [POST("admin/bodyregions", IsAbsoluteUrl = true)]
        public ActionResult AddBodyRegion(BodyRegion bodyRegion)
        {
            var response = _adminService.AddBodyRegion(bodyRegion);
            bodyRegion.id = response;

            return Json(bodyRegion, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/bodyregions/{Id}", IsAbsoluteUrl = true)]
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

        [GET("admin/treatments", IsAbsoluteUrl = true)]
        public ActionResult GetTreatments()
        {
            var treatments = _adminService.GetTreatments();

            return Json(treatments, JsonRequestBehavior.AllowGet);
        }

        [POST("admin/treatments", IsAbsoluteUrl = true)]
        public ActionResult AddTreatment(Treatment treatment)
        {
            var response = _adminService.AddTreatment(treatment);
            treatment.id = response;

            return Json(treatment, JsonRequestBehavior.DenyGet);
        }

        [PUT("admin/treatments/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateTreatment(Treatment treatment)
        {
            _adminService.UpdateTreatment(treatment);

            return Json(treatment, JsonRequestBehavior.DenyGet);
        }


        #endregion


    }
}
