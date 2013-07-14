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

            return response.Resource.id;
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

            return response.Resource.id;
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

            return Mapper.Map<IEnumerable<Exercise>>(response.Resource.Items);
        }

        public int AddExercise(Exercise exercise)
        {
            var request = new ApiResourceRequest<ExerciseDto>
                {
                    Resource = Mapper.Map<ExerciseDto>(exercise)
                };

            var response = PostSync<ApiResourceRequest<ExerciseDto>>(_sportsWebPtClientSettings.ExercisePath, request);

            return response.Resource.id;
        }

        public void UpdateExercise(Exercise exercise)
        {
            var request = new ApiResourceRequest<ExerciseDto>
            {
                Resource = Mapper.Map<ExerciseDto>(exercise)
            };

            PutSync<ApiResourceRequest<ExerciseDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.ExercisePath, exercise.id), request);
        }

	    #endregion
    }
    
}
