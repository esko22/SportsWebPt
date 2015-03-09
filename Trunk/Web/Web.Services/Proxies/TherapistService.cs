using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface ITherapistService
    {
        IEnumerable<BriefExercise> GetExercises(String therapistId);
        IEnumerable<BriefPlan> GetPlans(String therapistId);
        Therapist GetTherapistDetail(String therapistId);
        bool UpdateTherapistSharedExercises(String therapistId, ClinicExercise[] sharedExercises);
        bool UpdateTherapistSharedPlans(String therapistId, ClinicPlan[] sharedPlans);
        IEnumerable<Case> GetCases(String therapistId, String state);
    }


    public class TherapistService : BaseServiceStackClient, ITherapistService 
    {
        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public TherapistService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion


        #region Methods

        public IEnumerable<BriefExercise> GetExercises(String therapistId)
        {
            var request = GetSync(new TherapistExerciseListRequest() { Id = therapistId, IsOwner = true});

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefExercise>>(request.Response.Items.OrderBy(p => p.Name));
        }

        public IEnumerable<Case> GetCases(String therapistId, String state)
        {
            CaseStateDto? caseState = null;
            if (!String.IsNullOrEmpty(state))
                caseState = (CaseStateDto)Enum.Parse(typeof(CaseStateDto), state, true);

            var request = GetSync(new TherapistCaseListRequest() { Id = therapistId, State = caseState });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Case>>(request.Response.Items.OrderBy(p => p.CreatedOn));
        }

        public IEnumerable<BriefPlan> GetPlans(String therapistId)
        {
            var request = GetSync(new TherapistPlanListRequest() { Id = therapistId, IsOwner = true });

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefPlan>>(request.Response.Items.OrderBy(p => p.RoutineName));
        }

        public Therapist GetTherapistDetail(String therapistId)
        {
            var request = GetSync(new TherapistRequest() { Id = therapistId});

            return request.Response == null ? null : Mapper.Map<Therapist>(request.Response);
        }

        public bool UpdateTherapistSharedExercises(String therapistId, ClinicExercise[] sharedExercises)
        {
            var request = new UpdateTherapistSharedExerciseRequest()
            {
                Id = therapistId,
                SharedExercises = Mapper.Map<ClinicExerciseDto[]>(sharedExercises)
            };

            Put(request);

            return true;
        }

        public bool UpdateTherapistSharedPlans(String therapistId, ClinicPlan[] sharedPlans)
        {
            var request = new UpdateTherapistSharedPlanRequest()
            {
                Id = therapistId,
                SharedPlans = Mapper.Map<ClinicPlanDto[]>(sharedPlans)
            };

            Put(request);

            return true;
        }


        #endregion

    }
}
