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


namespace SportsWebPt.Platform.Web.Research
{
    [RouteArea("Research")]
    public class ResearchController : BaseController
    {

        #region Fields

        private readonly IResearchService _researchService; 

        #endregion

        #region Construction

        public ResearchController(IResearchService researchService)
        {
            Check.Argument.IsNotNull(researchService, "Examine Service");
            _researchService = researchService;
        }

        #endregion

        #region Methods

        [GET("Research/workouts/{Id}", IsAbsoluteUrl = true)]
        public ActionResult GetWorkout(string Id)
        {
            var workoutId = 0;
            if (!String.IsNullOrEmpty(Id))
                int.TryParse(Id, out workoutId);

            var workout = _researchService.GetWorkout(workoutId);

            return Json(workout, JsonRequestBehavior.AllowGet);
        }


        [GET("Research", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            var viewModel = CreateViewModel<ResearchViewModel>();

            return View(viewModel);
        }


        [GET("Research/equipment", IsAbsoluteUrl = true)]
        public ActionResult GetEquipment()
        {
            var equipment = _researchService.GetEquipment();

            return Json(equipment, JsonRequestBehavior.AllowGet);
        }

        [POST("Research/equipment", IsAbsoluteUrl = true)]
        public ActionResult AddEquipment(Equipment equipment)
        {
            var response = _researchService.AddEquipment(equipment);
            equipment.id = response;

            return Json(equipment, JsonRequestBehavior.DenyGet);
        }

        [PUT("Research/equipment/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateEquipment(Equipment equipment)
        {
            _researchService.UpdateEquipment(equipment);

            return Json(equipment, JsonRequestBehavior.DenyGet);
        }

        [GET("Research/videos", IsAbsoluteUrl = true)]
        public ActionResult GetVideos()
        {
            var videos = _researchService.GetVideos();

            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        [POST("Research/videos", IsAbsoluteUrl = true)]
        public ActionResult AddVideo(Video video)
        {
            var response = _researchService.AddVideo(video);
            video.id = response;

            return Json(video, JsonRequestBehavior.DenyGet);
        }

        [PUT("Research/videos/{Id}", IsAbsoluteUrl = true)]
        public ActionResult UpdateVideo(Video video)
        {
            _researchService.UpdateVideo(video);

            return Json(video, JsonRequestBehavior.DenyGet);
        }


        #endregion

    }
}
