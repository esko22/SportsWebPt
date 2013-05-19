using System;
using AutoMapper;

using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class ResearchService : BaseServiceStackClient, IResearchService
    {
        #region Fields

        private readonly String _workoutPath = String.Empty;

        #endregion

        #region Construction

        public ResearchService(BaseServiceStackClientSettings clientSettings)
            :base(clientSettings)
        {
            _workoutPath = String.Format("/{0}/workouts", _settings.Version);
        }

        #endregion

        public Workout GetWorkout(int workoutId)
        {
            var response =
                GetSync<ApiResourceRequest<WorkoutDto>>(String.Format("{0}/{1}", _workoutPath, workoutId));

            var workout = Mapper.Map<Workout>(response.Resource);

            return workout;
        }
    }
}
