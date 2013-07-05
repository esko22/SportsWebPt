using System;
using System.Collections.Generic;
using AutoMapper;

using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services.Proxies;

namespace SportsWebPt.Platform.Web.Services
{
    public class ResearchService : BaseServiceStackClient, IResearchService
    {
        #region Fields

        private readonly String _workoutPath = String.Empty;
        private readonly String _equipmentPath = String.Empty;
        private readonly String _videoPath = String.Empty;

        #endregion

        #region Construction

        public ResearchService(BaseServiceStackClientSettings clientSettings)
            : base(clientSettings)
        {
            _workoutPath = String.Format("/{0}/workouts", _settings.Version);
            _equipmentPath = String.Format("/{0}/equipment", _settings.Version);
            _videoPath = String.Format("/{0}/videos", _settings.Version);
        }

        #endregion

        public Workout GetWorkout(int workoutId)
        {
            var response =
                GetSync<ApiResourceRequest<WorkoutDto>>(String.Format("{0}/{1}", _workoutPath, workoutId));

            var workout = Mapper.Map<Workout>(response.Resource);

            return workout;
        }


        public IEnumerable<Equipment> GetEquipment()
        {
            var response = GetSync<ListResponse<EquipmentDto, BasicSortBy>>(_equipmentPath);

            return Mapper.Map<IEnumerable<Equipment>>(response.Resource.Items);
        }

        public IEnumerable<Video> GetVideos()
        {
            var response = GetSync<ListResponse<VideoDto, BasicSortBy>>(_videoPath);

            return Mapper.Map<IEnumerable<Video>>(response.Resource.Items);
        }


        public int AddEquipment(Equipment equipment)
        {
            var equipmentResuest = new ApiResourceRequest<EquipmentDto>
                {
                    Resource = Mapper.Map<EquipmentDto>(equipment)
                };

            var response =
                PostSync<UserResourceResponse>(_equipmentPath, equipmentResuest);

            return response.Resource.id;
        }
    }
}
