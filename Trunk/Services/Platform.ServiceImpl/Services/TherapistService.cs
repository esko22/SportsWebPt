using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class TherapistService : RestService
    {
        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }
        public IPlanUnitOfWork PlanUnitOfWork { get; set; }
        public IExerciseUnitOfWork ExerciseUnitOfWork { get; set; }
        public ICaseUnitOfWork CaseUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(TherapistRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Id, "TherapistId");

            var therapist = UserUnitOfWork.UserRepository.GetTherapistDetails().SingleOrDefault(p => p.Id == new Guid(request.Id));

            return Ok(new ApiResponse<TherapistDto>() { Response = Mapper.Map<TherapistDto>(therapist) });
        }

        public object Get(TherapistPlanListRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Id, "TherapistId");

            var responseList = new List<BriefPlanDto>();
            var plans = PlanUnitOfWork.GetPlansByTherapist(new Guid(request.Id)).ToList().OrderBy(p => p.RoutineName);

            Mapper.Map(plans, responseList);

            return
                Ok(new ApiListResponse<BriefPlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(TherapistExerciseListRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Id, "TherapistId");

            var responseList = new List<BriefExerciseDto>();
            var exercises =
                ExerciseUnitOfWork.GetExercisesByTherapist(new Guid(request.Id)).ToList().OrderBy(p => p.Name);

            Mapper.Map(exercises, responseList);

            return
                Ok(new ApiListResponse<BriefExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Get(TherapistCaseListRequest request)
        {
            var responseList = new List<CaseDto>();
            var cases = CaseUnitOfWork.GetFilteredCases(request.Id, state: request.State.ToString());

            Mapper.Map(cases, responseList);

            return
                Ok(new ApiListResponse<CaseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Put(UpdateTherapistSharedPlanRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Id, "TherapistId");

            if (request.SharedPlans.Any())
                PlanUnitOfWork.UpdateSharedPlans(Mapper.Map<IEnumerable<ClinicPlanMatrixItem>>(request.SharedPlans));

            return Ok();
        }

        public object Put(UpdateTherapistSharedExerciseRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Id, "TherapistId");

            if (request.SharedExercises.Any())
                ExerciseUnitOfWork.UpdateSharedExercises(Mapper.Map<IEnumerable<ClinicExerciseMatrixItem>>(request.SharedExercises));

            return Ok();
        }

        #endregion

    }
}
