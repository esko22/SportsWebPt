using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class AdminService : BaseServiceStackClient, IAdminService
    {
        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;
        
        #endregion
        
        #region Construction

        public AdminService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion

        #region Methods

        public IEnumerable<Equipment> GetEquipment()
        {
            var request = GetSync(new EquipmentListRequest());

            return Mapper.Map<IEnumerable<Equipment>>(request.Response.Items);
        }

        public IEnumerable<Video> GetVideos()
        {
            var request = GetSync(new VideoListRequest());

            return Mapper.Map<IEnumerable<Video>>(request.Response.Items);
        }


        public int AddEquipment(Equipment equipment)
        {
            var request = PostSync(Mapper.Map<CreateEquipmentRequest>(equipment));

            return request.Response.Id;
        }

        public void UpdateEquipment(Equipment equipment)
        {
            Put(Mapper.Map<UpdateEquipmentRequest>(equipment));
        }

        public int AddVideo(Video video)
        {
            var request = PostSync(Mapper.Map<CreateVideoRequest>(video));

            return request.Response.Id;
        }

        public void UpdateVideo(Video video)
        {
            Put(Mapper.Map<UpdateVideoRequest>(video));
        }

        public IEnumerable<Exercise> GetExercises()
        {
            var request = GetSync(new ExerciseListRequest());

            return Mapper.Map<IEnumerable<Exercise>>(request.Response.Items);
        }

        public int AddExercise(Exercise exercise)
        {
            var request = PostSync(Mapper.Map<CreateExerciseRequest>(exercise));

            return request.Response.Id;
        }

        public void UpdateExercise(Exercise exercise)
        {
            Put(Mapper.Map<UpdateExerciseRequest>(exercise));
        }

        public IEnumerable<GridPlan> GetPlans()
        {
            var request = GetSync(new BriefPlanListRequest() { ClinicId = WebPlatformConfigSettings.Instance.SportsWebPtClinicId });

            return request.Response == null ? null : Mapper.Map<IEnumerable<GridPlan>>(request.Response.Items.OrderBy(p => p.RoutineName));
        }

        public int AddPlan(Plan plan)
        {
            var request = PostSync(Mapper.Map<CreatePlanRequest>(plan));

            return request.Response.Id;
        }

        public void UpdatePlan(Plan plan)
        {
            Put(Mapper.Map<UpdatePlanRequest>(plan));
        }

        public void PublishPlan(Plan plan)
        {
            Patch(Mapper.Map<PublishPlanRequest>(plan));
        }

        public IEnumerable<Injury> GetInjuries()
        {
            var request = GetSync(new InjuryListRequest());

            return Mapper.Map<IEnumerable<Injury>>(request.Response.Items);
        }

        public int AddInjury(Injury injury)
        {
            var request = PostSync(Mapper.Map<CreateInjuryRequest>(injury));

            return request.Response.Id;
        }

        public void UpdateInjury(Injury injury)
        {
            Put(Mapper.Map<UpdateInjuryRequest>(injury));
        }

        public IEnumerable<Sign> GetSigns()
        {
            var request = GetSync(new SignListRequest());

            return Mapper.Map<IEnumerable<Sign>>(request.Response.Items);
        }

        public int AddSign(Sign sign)
        {
            var request = PostSync(Mapper.Map<CreateSignRequest>(sign));

            return request.Response.Id;
        }

        public void UpdateSign(Sign sign)
        {
            Put(Mapper.Map<UpdateSignRequest>(sign));
        }

        public IEnumerable<Cause> GetCauses()
        {
            var request = GetSync(new CauseListRequest());

            return Mapper.Map<IEnumerable<Cause>>(request.Response.Items);
        }

        public int AddCause(Cause cause)
        {
            var request = PostSync(Mapper.Map<CreateCauseRequest>(cause));

            return request.Response.Id;
        }

        public void UpdateCause(Cause cause)
        {
            Put(Mapper.Map<UpdateCauseRequest>(cause));
        }

        public IEnumerable<BodyPartMatrixItem> GetBodyPartMatrix()
        {
            var request = GetSync(new BodyPartMatrixListRequest());

            return Mapper.Map<IEnumerable<BodyPartMatrixItem>>(request.Response.Items);
        }

        public IEnumerable<Symptom> GetSymtpoms()
        {
            var request = GetSync(new SymptomListRequest());

            return Mapper.Map<IEnumerable<Symptom>>(request.Response.Items);
        }

        public IEnumerable<BodyPart> GetBodyParts()
        {
            var request = GetSync(new BodyPartListRequest());

            return Mapper.Map<IEnumerable<BodyPart>>(request.Response.Items);
        }

        public IEnumerable<SkeletonArea> GetSkeletonAreas()
        {
            var request = GetSync(new SkeletonAreaListRequest());

            return Mapper.Map<IEnumerable<SkeletonArea>>(request.Response.Items);
        }

        public int AddBodyPart(BodyPart bodyPart)
        {
            var request = PostSync(Mapper.Map<CreateBodyPartRequest>(bodyPart));

            return request.Response.Id;
        }

        public void UpdateBodyPart(BodyPart bodyPart)
        {
            Put(Mapper.Map<UpdateBodyPartRequest>(bodyPart));
        }

        public IEnumerable<BodyRegion> GetBodyRegions()
        {
            var request = GetSync(new BodyRegionListRequest());

            return Mapper.Map<IEnumerable<BodyRegion>>(request.Response.Items);
        }

        public int AddBodyRegion(BodyRegion bodyRegion)
        {
            var request = PostSync(Mapper.Map<CreateBodyRegionRequest>(bodyRegion));

            return request.Response.Id;
        }

        public void UpdateBodyRegion(BodyRegion bodyRegion)
        {
            Put(Mapper.Map<UpdateBodyRegionRequest>(bodyRegion));
        }

        public bool ValidatePageName(String pageName)
        {
            return GetSync(new PageNameValidationRequest()
                {
                    PageName = pageName
                });
        }

        public IEnumerable<Treatment> GetTreatments()
        {
            var request = GetSync(new TreatmentListRequest());

            return Mapper.Map<IEnumerable<Treatment>>(request.Response.Items);
        }

        public int AddTreatment(Treatment treatment)
        {
            var request = PostSync(Mapper.Map<CreateTreatmentRequest>(treatment));

            return request.Response.Id;
        }

        public void UpdateTreatment(Treatment treatment)
        {
            Put(Mapper.Map<UpdateTreatmentRequest>(treatment));
        }

        public IEnumerable<Prognosis> GetPrognoses()
        {
            var request = GetSync(new PrognosisListRequest());

            return Mapper.Map<IEnumerable<Prognosis>>(request.Response.Items);
        }

        public int AddPrognosis(Prognosis prognosis)
        {
            var request = PostSync(Mapper.Map<CreatePrognosisRequest>(prognosis));

            return request.Response.Id;
        }

        public void UpdatePrognosis(Prognosis prognosis)
        {
            Put(Mapper.Map<UpdatePrognosisRequest>(prognosis));
        }


        #endregion

    }
    
}
