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
        IEnumerable<GridExercise> GetExercises(String therapistId);
        IEnumerable<GridPlan> GetPlans(String therapistId);
        Therapist GetTherapistDetail(String therapistId);
        IEnumerable<TherapistSharedExercise> GetTherapistSharedExercises(String therapistId, int? exerciseId);
        IEnumerable<TherapistSharedPlan> GetTherapistSharedPlans(String therapistId, int? planId);
        bool UpdateTherapistSharedExercises(String therapistId, TherapistSharedExercise[] sharedExercises);
        bool UpdateTherapistSharedPlans(String therapistId, TherapistSharedPlan[] sharedPlans);
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
        
        public IEnumerable<GridExercise> GetExercises(String therapistId)
        {
            var request = GetSync(new TherapistExerciseListRequest() { Id = therapistId, IsOwner = true});

            return request.Response == null ? null : Mapper.Map<IEnumerable<GridExercise>>(request.Response.Items.OrderBy(p => p.Name));

        }

        public IEnumerable<Case> GetCases(String therapistId, String state)
        {
            CaseStateDto? caseState = null;
            if (!String.IsNullOrEmpty(state))
                caseState = (CaseStateDto)Enum.Parse(typeof(CaseStateDto), state, true);

            var request = GetSync(new TherapistCaseListRequest() { Id = therapistId, State = caseState });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Case>>(request.Response.Items.OrderBy(p => p.CreatedOn));
        }

        public IEnumerable<GridPlan> GetPlans(String therapistId)
        {
            var request = GetSync(new TherapistPlanListRequest() { Id = therapistId, IsOwner = true });

            return request.Response == null ? null : Mapper.Map<IEnumerable<GridPlan>>(request.Response.Items.OrderBy(p => p.RoutineName));
        }

        public Therapist GetTherapistDetail(String therapistId)
        {
            var request = GetSync(new TherapistRequest() { Id = therapistId});

            return request.Response == null ? null : Mapper.Map<Therapist>(request.Response);
        }

        public IEnumerable<TherapistSharedExercise> GetTherapistSharedExercises(String therapistId, int? exerciseId)
        {
            var request = GetSync(new TherapistSharedExerciseListRequest() { Id = therapistId, ExerciseId = exerciseId ?? 0});
            return request.Response == null ? null : Mapper.Map<IEnumerable<TherapistSharedExercise>>(request.Response.Items);
        }

        public IEnumerable<TherapistSharedPlan> GetTherapistSharedPlans(String therapistId, int? planId)
        {
            var request = GetSync(new TherapistSharedPlanListRequest() { Id = therapistId, PlanId = planId ?? 0 });

            return request.Response == null ? null : Mapper.Map<IEnumerable<TherapistSharedPlan>>(request.Response.Items);
        }

        public bool UpdateTherapistSharedExercises(String therapistId, TherapistSharedExercise[] sharedExercises)
        {
            var request = new UpdateTherapistSharedExerciseRequest()
            {
                Id = therapistId,
                SharedExercises = Mapper.Map<TherapistSharedExerciseDto[]>(sharedExercises)
            };

            Put(request);

            return true;
        }

        public bool UpdateTherapistSharedPlans(String therapistId, TherapistSharedPlan[] sharedPlans)
        {
            var request = new UpdateTherapistSharedPlanRequest()
            {
                Id = therapistId.ToString(),
                SharedPlans = Mapper.Map<TherapistSharedPlanDto[]>(sharedPlans)
            };

            Put(request);

            return true;
        }


        #endregion

    }
}
