﻿using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using ServiceStack.Common.Extensions;
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

        public Plan GetPlan(int planId, String requestorId)
        {
            var request =
                GetSync(new PlanRequest() { Id = planId.ToString(), RequestorId = requestorId});

            var plan = Mapper.Map<Plan>(request.Response);

            return plan;
        }

        public Exercise GetExercise(int exerciseId)
        {
            var request =
                GetSync(new ExerciseRequest() { Id = exerciseId.ToString() });

            var exercise = Mapper.Map<Exercise>(request.Response);

            return exercise;
        }

        public Injury GetInjury(int injuryId)
        {
            var request =
                GetSync(new InjuryRequest() { Id = injuryId.ToString() });

            var injury = Mapper.Map<Injury>(request.Response);

            return injury;
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

        public IEnumerable<BriefExercise> GetResearchExercises()
        {
            var request = GetSync(new BriefExerciseListRequest() { ClinicId = WebPlatformConfigSettings.Instance.SportsWebPtClinicId, IsPublic = true });

            //TODO: hack due to angular not filtering on nulls
            request.Response.Items.ForEach(e =>
            {
                if (!e.Equipment.Any())
                    e.Equipment = new [] { new EquipmentDto() { CommonName = "NA", Id = 0 } };
            });

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefExercise>>(request.Response.Items.OrderBy(p => p.Name));
        }

        public IEnumerable<BriefPlan> GetResearchPlans()
        {
            var request = GetSync(new BriefPlanListRequest() { ClinicId = WebPlatformConfigSettings.Instance.SportsWebPtClinicId, IsPublic = true });

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefPlan>>(request.Response.Items.OrderBy(p => p.RoutineName));
        }
     
        public IEnumerable<Equipment> GetEquipment()
        {
            var request = GetSync(new EquipmentListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<Equipment>>(request.Response.Items);
        }

        public IEnumerable<BriefEquipment> GetBriefEquipment()
        {
            var request = GetSync(new BriefEquipmentListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefEquipment>>(request.Response.Items.OrderBy(p => p.CommonName));
        }

        public IEnumerable<Injury> GetInjuries()
        {
            var request = GetSync(new InjuryListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<Injury>>(request.Response.Items);
        }

        public IEnumerable<BriefInjury> GetResearchInjuries()
        {
            var request = GetSync(new BriefInjuryListRequest() { ClinicId = WebPlatformConfigSettings.Instance.SportsWebPtClinicId, IsPublic = true});

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefInjury>>(request.Response.Items.OrderBy(p => p.CommonName));
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

    public interface IResearchService
    {
        Plan GetPlan(int planId, String requestorId);
        Exercise GetExercise(int exerciseId);
        Injury GetInjury(int injuryId);
        IEnumerable<BodyPart> GetBodyParts();
        IEnumerable<BodyRegion> GetBodyRegions();
        IEnumerable<BriefExercise> GetResearchExercises();
        IEnumerable<BriefPlan> GetResearchPlans();
        IEnumerable<BriefInjury> GetResearchInjuries();
        IEnumerable<Sign> GetSigns();
        IEnumerable<Equipment> GetEquipment();
        IEnumerable<BriefEquipment> GetBriefEquipment();
        Exercise GetExerciseByPageName(string pageName);
        Injury GetInjuryByPageName(string pageName);
        Plan GetPlanByPageName(string pageName);
    }
}
