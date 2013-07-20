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

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public ResearchService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion

        public Workout GetWorkout(int workoutId)
        {
            var response =
                GetSync<ApiResourceRequest<WorkoutDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.WorkoutPath,
                                                                      workoutId));

            var workout = Mapper.Map<Workout>(response.Resource);

            return workout;
        }

        public IEnumerable<BodyPart> GetBodyParts()
        {
            var response =
                GetSync<ListResponse<BodyPartDto, BodyPartSortBy>>(_sportsWebPtClientSettings.BodyPartUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<BodyPart>>(response.Resource.Items);
        }

        public IEnumerable<BodyRegion> GetBodyRegions()
        {
            var response =
                GetSync<ListResponse<BodyRegionDto, BasicSortBy>>(_sportsWebPtClientSettings.BodyRegionUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<BodyRegion>>(response.Resource.Items);
        }

        public IEnumerable<Exercise> GetExercises()
        {
            var response = GetSync<ListResponse<ExerciseDto, BasicSortBy>>(_sportsWebPtClientSettings.ExercisePath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<Exercise>>(response.Resource.Items);
        }

        public Exercise GetExerciseByPageName(string pageName)
        {
            var response =
                 GetSync<ApiResourceRequest<ExerciseDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.ExercisePath,pageName));

            var exercise = Mapper.Map<Exercise>(response.Resource);

            return exercise;
        }

    }
}
