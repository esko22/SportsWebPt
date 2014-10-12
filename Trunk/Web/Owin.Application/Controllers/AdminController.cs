using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Admin
{
    public class AdminController : ApiController
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

        [HttpGet]
        [Route("data/admin/equipment")]
        public IEnumerable<Equipment> GetEquipment()
        {
            return _adminService.GetEquipment();
        }

        [HttpPost]
        [Route("data/admin/equipment")]
        public Equipment AddEquipment(Equipment equipment)
        {
            var response = _adminService.AddEquipment(equipment);
            equipment.id = response;

            return equipment;
        }

        [HttpPut]
        [Route("data/admin/equipment/{Id}")]
        public Equipment UpdateEquipment(Equipment equipment)
        {
            _adminService.UpdateEquipment(equipment);

            return equipment;
        }

        [HttpGet]
        [Route("data/admin/videos")]
        public IEnumerable<Video> GetVideos()
        {
            return _adminService.GetVideos();
        }

        [HttpPost]
        [Route("data/admin/videos")]
        public Video AddVideo(Video video)
        {
            var response = _adminService.AddVideo(video);
            video.id = response;

            return video;
        }

        [HttpPut]
        [Route("data/admin/videos/{Id}")]
        public Video UpdateVideo(Video video)
        {
            _adminService.UpdateVideo(video);

            return video;
        }

        [HttpGet]
        [Route("data/admin/exercises")]
        public IEnumerable<GridExercise> GetExercises()
        {
            return _adminService.GetClinicExercises();
        }

        [HttpGet]
        [Route("data/admin/exercises/{id}")]
        public Exercise GetExercise(Int32 id)
        {
            return _researchService.GetExercise(id);
        }

        [HttpGet]
        [Route("data/admin/injuries/{id}")]
        public Injury GetInjury(Int32 id)
        {
            return _researchService.GetInjury(id);
        }

        [HttpPost]
        [Route("data/admin/exercises")]
        public Exercise AddExercise(Exercise exercise)
        {
            var result = _adminService.AddExercise(exercise, Convert.ToInt32(User.Identity.Name));
            exercise.id = result;
            return exercise;
        }

        [HttpPut]
        [Route("data/admin/exercises/{Id}")]
        public Exercise UpdateExercise(Exercise exercise)
        {
            _adminService.UpdateExercise(exercise);
            return exercise;
        }

        [HttpGet]
        [Route("data/admin/bodyregions")]
        public IEnumerable<BodyRegion> GetBodyRegions()
        {
            return _researchService.GetBodyRegions();
        }

        [HttpGet]
        [Route("data/admin/plans")]
        public IEnumerable<GridPlan> GetPlans()
        {
            return _adminService.GetClinicPlans();
        }

        [HttpGet]
        [Route("data/admin/plans/{id}")]
        public Plan GetPlan(Int32 id)
        {
            return _researchService.GetPlan(id);
        }

        [HttpGet]
        [Route("data/admin/bodypartmatrix")]
        public IEnumerable<BodyPartMatrixItem> GetBodyPartMatrix()
        {
            return _adminService.GetBodyPartMatrix();
        }

        [HttpGet]
        [Route("data/admin/symptoms")]
        public IEnumerable<Symptom> GetSymptoms()
        {
            return _adminService.GetSymtpoms();
        }

        [HttpPost]
        [Route("data/admin/plans")]
        public Plan AddPlan(Plan plan)
        {
            var result = _adminService.AddPlan(plan, Convert.ToInt32(User.Identity.Name));
            plan.id = result;

            return plan;
        }

        [HttpPut]
        [Route("data/admin/plans/{Id}")]
        public Plan UpdatePlan(Plan plan)
        {
            _adminService.UpdatePlan(plan);
            return plan;
        }

        [HttpPut]
        [Route("data/admin/plans/{Id}/publish")]
        public Plan PublishPlan(Plan plan)
        {
            _adminService.PublishPlan(plan);
            return plan;
        }

        [HttpPut]
        [Route("data/admin/injuries/{Id}/publish")]
        public Injury PublishInjury(Injury injury)
        {
            _adminService.PublishInjury(injury);
            return injury;
        }

        [HttpPut]
        [Route("data/admin/exercises/{Id}/publish")]
        public Exercise PublishExercise(Exercise exercise)
        {
            _adminService.PublishExercise(exercise);
            return exercise;
        }

        [HttpGet]
        [Route("data/admin/signs")]
        public IEnumerable<Sign> GetSigns()
        {
            return _adminService.GetSigns();
        }

        [HttpPost]
        [Route("data/admin/signs")]
        public Sign AddSign(Sign sign)
        {
            var response = _adminService.AddSign(sign);
            sign.id = response;

            return sign;
        }

        [HttpPut]
        [Route("data/admin/signs/{Id}")]
        public Sign UpdateSign(Sign sign)
        {
            _adminService.UpdateSign(sign);

            return sign;
        }


        [HttpGet]
        [Route("data/admin/causes")]
        public IEnumerable<Cause> GetCauses()
        {
            return _adminService.GetCauses();
        }

        [HttpPost]
        [Route("data/admin/causes")]
        public Cause AddCause(Cause cause)
        {
            var response = _adminService.AddCause(cause);
            cause.id = response;

            return cause;
        }

        [HttpPut]
        [Route("data/admin/causes/{Id}")]
        public Cause UpdateCause(Cause cause)
        {
            _adminService.UpdateCause(cause);

            return cause;
        }

        [HttpGet]
        [Route("data/admin/injuries")]
        public IEnumerable<GridInjury> GetInjuries()
        {
            return _adminService.GetClinicInjuries();
        }

        [HttpPost]
        [Route("data/admin/injuries")]
        public Injury AddInjury(Injury injury)
        {
            var response = _adminService.AddInjury(injury);
            injury.id = response;

            return injury;
        }

        [HttpPut]
        [Route("data/admin/injuries/{Id}")]
        public Injury UpdateInjury(Injury injury)
        {
            _adminService.UpdateInjury(injury);

            return injury;
        }

        [HttpGet]
        [Route("data/admin/bodyparts")]
        public IEnumerable<BodyPart> GetBodyParts()
        {
            return _adminService.GetBodyParts();
        }

        [HttpGet]
        [Route("data/admin/skeletonareas")]
        public IEnumerable<SkeletonArea> GetSkeletonAreas()
        {
            return _adminService.GetSkeletonAreas();
        }

        [HttpPost]
        [Route("data/admin/bodyparts")]
        public BodyPart AddBodyPart(BodyPart bodyPart)
        {
            var response = _adminService.AddBodyPart(bodyPart);
            bodyPart.id = response;

            return bodyPart;
        }

        [HttpPut]
        [Route("data/admin/bodyparts/{Id}")]
        public BodyPart UpdateBodyPart(BodyPart bodyPart)
        {
            _adminService.UpdateBodyPart(bodyPart);

            return bodyPart;
        }

        [HttpPost]
        [Route("data/admin/bodyregions")]
        public BodyRegion AddBodyRegion(BodyRegion bodyRegion)
        {
            var response = _adminService.AddBodyRegion(bodyRegion);
            bodyRegion.id = response;

            return bodyRegion;
        }

        [HttpPut]
        [Route("data/admin/bodyregions/{Id}")]
        public BodyRegion UpdateBodyRegion(BodyRegion bodyRegion)
        {
            _adminService.UpdateBodyRegion(bodyRegion);

            return bodyRegion;
        }

        [HttpGet]
        [Route("validate/pagename")]
        public Boolean ValidatePageName(string pageName)
        {
            return _adminService.ValidatePageName(pageName);
        }

        [HttpGet]
        [Route("data/admin/treatments")]
        public IEnumerable<Treatment> GetTreatments()
        {
            return _adminService.GetTreatments();
        }

        [HttpPost]
        [Route("data/admin/treatments")]
        public Treatment AddTreatment(Treatment treatment)
        {
            var response = _adminService.AddTreatment(treatment);
            treatment.id = response;

            return treatment;
        }

        [HttpPut]
        [Route("data/admin/treatments/{Id}")]
        public Treatment UpdateTreatment(Treatment treatment)
        {
            _adminService.UpdateTreatment(treatment);

            return treatment;
        }

        [HttpGet]
        [Route("data/admin/prognoses")]
        public IEnumerable<Prognosis> GetPrognoses()
        {
            return _adminService.GetPrognoses();
        }

        [HttpPost]
        [Route("data/admin/prognoses")]
        public Prognosis AddPrognosis(Prognosis prognosis)
        {
            var response = _adminService.AddPrognosis(prognosis);
            prognosis.id = response;

            return prognosis;
        }

        [HttpPut]
        [Route("data/admin/prognoses/{Id}")]
        public Prognosis UpdatePrognosis(Prognosis prognosis)
        {
            _adminService.UpdatePrognosis(prognosis);

            return prognosis;
        }


        #endregion


    }
}
