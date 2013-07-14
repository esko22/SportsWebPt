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


        #endregion


    }
}
