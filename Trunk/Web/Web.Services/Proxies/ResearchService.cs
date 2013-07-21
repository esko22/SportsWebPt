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

        public Plan GetPlan(int planId)
        {
            var response =
                GetSync<ApiResourceRequest<PlanDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.PlanPath,
                                                                      planId));

            var plan = Mapper.Map<Plan>(response.Resource);

            return plan;
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

        public IEnumerable<Sign> GetSigns()
        {
            var response =
                GetSync<ListResponse<SignDto, BasicSortBy>>(_sportsWebPtClientSettings.SignPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<Sign>>(response.Resource.Items);
        }

        public IEnumerable<Exercise> GetExercises()
        {
            var response = GetSync<ListResponse<ExerciseDto, BasicSortBy>>(_sportsWebPtClientSettings.ExercisePath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<Exercise>>(response.Resource.Items);
        }

        public IEnumerable<Plan> GetPlans()
        {
            var response = GetSync<ListResponse<PlanDto, BasicSortBy>>(_sportsWebPtClientSettings.PlanPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<Plan>>(response.Resource.Items);
        }

        public IEnumerable<Injury> GetInjuries()
        {
            var response = GetSync<ListResponse<InjuryDto, BasicSortBy>>(_sportsWebPtClientSettings.InjuryPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<Injury>>(response.Resource.Items);
        }

        public Exercise GetExerciseByPageName(string pageName)
        {
            var response =
                 GetSync<ApiResourceRequest<ExerciseDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.ExercisePath,pageName));

            var exercise = Mapper.Map<Exercise>(response.Resource);

            return exercise;
        }

        public Injury GetInjuryByPageName(string pageName)
        {
            var response =
                 GetSync<ApiResourceRequest<InjuryDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.InjuryPath, pageName));

            var injury = Mapper.Map<Injury>(response.Resource);

            return injury;
        }

        public Plan GetPlanByPageName(string pageName)
        {
            var response =
                 GetSync<ApiResourceRequest<PlanDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.PlanPath, pageName));

            var plan = Mapper.Map<Plan>(response.Resource);

            return plan;
            
        }


    }
}
