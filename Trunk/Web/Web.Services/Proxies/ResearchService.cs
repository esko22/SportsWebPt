using System;
using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

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
            var request =
                GetSync(new PlanRequest() { Id = planId.ToString() });

            var plan = Mapper.Map<Plan>(request.Response);

            return plan;
        }

        public IEnumerable<BodyPart> GetBodyParts()
        {
            var request = GetSync(new BodyPartListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<BodyPart>>(request.Response.Items);
        }

        public IEnumerable<BodyRegion> GetBodyRegions()
        {
            var request = GetSync(new BodyRegionListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<BodyRegion>>(request.Response.Items);
        }

        public IEnumerable<Sign> GetSigns()
        {
            var request = GetSync(new SignListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<Sign>>(request.Response.Items);
        }

        public IEnumerable<Exercise> GetExercises()
        {
            var request = GetSync(new ExerciseListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<Exercise>>(request.Response.Items);
        }

        public IEnumerable<Plan> GetPlans()
        {
            var request = GetSync(new PlanListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<Plan>>(request.Response.Items);
        }

        public IEnumerable<Injury> GetInjuries()
        {
            var request = GetSync(new InjuryListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<Injury>>(request.Response.Items);
        }

        public Exercise GetExerciseByPageName(string pageName)
        {
            var request = GetSync(new ExerciseRequest() {Id = pageName} );

            var exercise = Mapper.Map<Exercise>(request.Response);

            return exercise;
        }

        public Injury GetInjuryByPageName(string pageName)
        {
            var request = GetSync(new InjuryRequest() {Id = pageName});

            var injury = Mapper.Map<Injury>(request.Response);

            return injury;
        }

        public Plan GetPlanByPageName(string pageName)
        {
            var request = GetSync(new PlanRequest() {Id = pageName});

            var plan = Mapper.Map<Plan>(request.Response);

            return plan;
            
        }


    }
}
