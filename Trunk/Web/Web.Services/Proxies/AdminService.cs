using System;
using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services.Proxies;

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
            var response = GetSync<ListResponse<EquipmentDto, BasicSortBy>>(_sportsWebPtClientSettings.EquipmentPath);

            return Mapper.Map<IEnumerable<Equipment>>(response.Resource.Items);
        }

        public IEnumerable<Video> GetVideos()
        {
            var response = GetSync<ListResponse<VideoDto, BasicSortBy>>(_sportsWebPtClientSettings.VideoPath);

            return Mapper.Map<IEnumerable<Video>>(response.Resource.Items);
        }


        public int AddEquipment(Equipment equipment)
        {
            var equipmentRequest = new ApiResourceRequest<EquipmentDto>
                {
                    Resource = Mapper.Map<EquipmentDto>(equipment)
                };

            var response =
                PostSync<EquipmentResourceResponse>(_sportsWebPtClientSettings.EquipmentPath, equipmentRequest);

            return response.Resource.Id;
        }

        public void UpdateEquipment(Equipment equipment)
        {
            var equipmentRequest = new ApiResourceRequest<EquipmentDto>
            {
                Resource = Mapper.Map<EquipmentDto>(equipment)
            };

            PutSync<EquipmentResourceResponse>(String.Format("{0}/{1}", _sportsWebPtClientSettings.EquipmentPath, equipment.id), equipmentRequest);
        }

        public int AddVideo(Video video)
        {
            var request = new ApiResourceRequest<VideoDto>
            {
                Resource = Mapper.Map<VideoDto>(video)
            };

            var response =
                PostSync<VideoResourceResponse>(_sportsWebPtClientSettings.VideoPath, request);

            return response.Resource.Id;
        }

        public void UpdateVideo(Video video)
        {
            var request = new ApiResourceRequest<VideoDto>
            {
                Resource = Mapper.Map<VideoDto>(video)
            };

            PutSync<VideoResourceResponse>(String.Format("{0}/{1}", _sportsWebPtClientSettings.VideoPath, video.id), request);
        }

        public IEnumerable<Exercise> GetExercises()
        {
            var response = GetSync<ListResponse<ExerciseDto, BasicSortBy>>(_sportsWebPtClientSettings.ExercisePath);
            var exercises = Mapper.Map<IEnumerable<Exercise>>(response.Resource.Items);

            return exercises;
        }

        public int AddExercise(Exercise exercise)
        {
            var request = new ApiResourceRequest<ExerciseDto>
                {
                    Resource = Mapper.Map<ExerciseDto>(exercise)
                };

            var response = PostSync<ApiResourceRequest<ExerciseDto>>(_sportsWebPtClientSettings.ExercisePath, request);

            return response.Resource.Id;
        }

        public void UpdateExercise(Exercise exercise)
        {
            var request = new ApiResourceRequest<ExerciseDto>
            {
                Resource = Mapper.Map<ExerciseDto>(exercise)
            };

            PutSync<ApiResourceRequest<ExerciseDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.ExercisePath, exercise.id), request);
        }

        public IEnumerable<Plan> GetPlans()
        {
            var response = GetSync<ListResponse<PlanDto, BasicSortBy>>(_sportsWebPtClientSettings.PlanPath);

            return Mapper.Map<IEnumerable<Plan>>(response.Resource.Items);
        }

        public int AddPlan(Plan plan)
        {
            var request = new ApiResourceRequest<PlanDto>
            {
                Resource = Mapper.Map<PlanDto>(plan)
            };

            var response = PostSync<ApiResourceRequest<PlanDto>>(_sportsWebPtClientSettings.PlanPath, request);

            return response.Resource.Id;
        }

        public void UpdatePlan(Plan plan)
        {
            var request = new ApiResourceRequest<PlanDto>
            {
                Resource = Mapper.Map<PlanDto>(plan)
            };

            PutSync<ApiResourceRequest<PlanDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.PlanPath, plan.id), request);
        }

        public IEnumerable<Injury> GetInjuries()
        {
            var response = GetSync<ListResponse<InjuryDto, BasicSortBy>>(_sportsWebPtClientSettings.InjuryPath);

            return Mapper.Map<IEnumerable<Injury>>(response.Resource.Items);
        }

        public int AddInjury(Injury injury)
        {
            var request = new ApiResourceRequest<InjuryDto>
            {
                Resource = Mapper.Map<InjuryDto>(injury)
            };

            var response =
                PostSync<InjuryResourceResponse>(_sportsWebPtClientSettings.InjuryPath, request);

            return response.Resource.id;
        }

        public void UpdateInjury(Injury injury)
        {
            var request = new ApiResourceRequest<InjuryDto>
            {
                Resource = Mapper.Map<InjuryDto>(injury)
            };

            PutSync<ApiResourceRequest<InjuryDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.InjuryPath, injury.id), request);
        }

        public IEnumerable<Sign> GetSigns()
        {
            var response = GetSync<ListResponse<SignDto, BasicSortBy>>(_sportsWebPtClientSettings.SignPath);

            return Mapper.Map<IEnumerable<Sign>>(response.Resource.Items);
        }

        public int AddSign(Sign sign)
        {
            var request = new ApiResourceRequest<SignDto>
            {
                Resource = Mapper.Map<SignDto>(sign)
            };

            var response =
                PostSync<SignResourceResponse>(_sportsWebPtClientSettings.SignPath, request);

            return response.Resource.id;
        }

        public void UpdateSign(Sign sign)
        {
            var request = new ApiResourceRequest<SignDto>
            {
                Resource = Mapper.Map<SignDto>(sign)
            };

            PutSync<SignResourceResponse>(String.Format("{0}/{1}", _sportsWebPtClientSettings.SignPath, sign.id), request);
        }

        public IEnumerable<Cause> GetCauses()
        {
            var response = GetSync<ListResponse<CauseDto, BasicSortBy>>(_sportsWebPtClientSettings.CausePath);

            return Mapper.Map<IEnumerable<Cause>>(response.Resource.Items);
        }

        public int AddCause(Cause cause)
        {
            var request = new ApiResourceRequest<CauseDto>
            {
                Resource = Mapper.Map<CauseDto>(cause)
            };

            var response =
                PostSync<CauseResourceResponse>(_sportsWebPtClientSettings.CausePath, request);

            return response.Resource.id;
        }

        public void UpdateCause(Cause cause)
        {
            var request = new ApiResourceRequest<CauseDto>
            {
                Resource = Mapper.Map<CauseDto>(cause)
            };

            PutSync<CauseResourceResponse>(String.Format("{0}/{1}", _sportsWebPtClientSettings.CausePath, cause.id), request);
        }

        public IEnumerable<BodyPartMatrixItem> GetBodyPartMatrix()
        {
            var response = GetSync<ListResponse<BodyPartMatrixItemDto, BasicSortBy>>(_sportsWebPtClientSettings.BodyPartMatrixPath);

            return Mapper.Map<IEnumerable<BodyPartMatrixItem>>(response.Resource.Items);
        }

        public IEnumerable<Symptom> GetSymtpoms()
        {
            var response = GetSync<ListResponse<SymptomDto, BasicSortBy>>(_sportsWebPtClientSettings.SymptomPath);

            return Mapper.Map<IEnumerable<Symptom>>(response.Resource.Items);
        }

        public IEnumerable<BodyPart> GetBodyParts()
        {
            var response = GetSync<ListResponse<BodyPart, BasicSortBy>>(_sportsWebPtClientSettings.BodyPartPath);

            return Mapper.Map<IEnumerable<BodyPart>>(response.Resource.Items);
        }

        public IEnumerable<SkeletonArea> GetSkeletonAreas()
        {
            var response = GetSync<ListResponse<SkeletonArea, BasicSortBy>>(_sportsWebPtClientSettings.SkeletonAreasUriPath);

            return Mapper.Map<IEnumerable<SkeletonArea>>(response.Resource.Items);
        }

        public int AddBodyPart(BodyPart bodyPart)
        {
            var request = new ApiResourceRequest<BodyPartDto>
            {
                Resource = Mapper.Map<BodyPartDto>(bodyPart)
            };

            var response =
                PostSync<BodyPartResourceResponse>(_sportsWebPtClientSettings.BodyPartPath, request);

            return response.Resource.Id;
        }

        public void UpdateBodyPart(BodyPart bodyPart)
        {
            var request = new ApiResourceRequest<BodyPartDto>
            {
                Resource = Mapper.Map<BodyPartDto>(bodyPart)
            };

            PutSync<BodyPartResourceResponse>(String.Format("{0}/{1}", _sportsWebPtClientSettings.BodyPartPath, bodyPart.id), request);
        }

        public IEnumerable<BodyRegion> GetBodyRegions()
        {
            var response = GetSync<ListResponse<BodyRegion, BasicSortBy>>(_sportsWebPtClientSettings.BodyRegionUriPath);

            return Mapper.Map<IEnumerable<BodyRegion>>(response.Resource.Items);
        }

        public int AddBodyRegion(BodyRegion bodyRegion)
        {
            var request = new ApiResourceRequest<BodyRegionDto>
            {
                Resource = Mapper.Map<BodyRegionDto>(bodyRegion)
            };

            var response =
                PostSync<BodyPartResourceResponse>(_sportsWebPtClientSettings.BodyRegionUriPath, request);

            return response.Resource.Id;
        }

        public void UpdateBodyRegion(BodyRegion bodyRegion)
        {
            var request = new ApiResourceRequest<BodyRegionDto>
            {
                Resource = Mapper.Map<BodyRegionDto>(bodyRegion)
            };

            PutSync<BodyPartResourceResponse>(String.Format("{0}/{1}", _sportsWebPtClientSettings.BodyRegionUriPath, bodyRegion.id), request);
        }

        #endregion

    }
    
}
