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
        public IEpisodeUnitOfWork EpisodeUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(TherapistRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "TherapistId");

            var therapist = UserUnitOfWork.UserRepository.GetTherapistDetails().SingleOrDefault(p => p.Id == request.IdAsInt);

            return Ok(new ApiResponse<TherapistDto>() { Response = Mapper.Map<TherapistDto>(therapist) });
        }

        public object Get(TherapistPlanListRequest request)
        {
            var responseList = new List<BriefPlanDto>();
            var plans = PlanUnitOfWork.PlanRepo.GetPlanDetails()
                .Where(p => p.TherapistPlanMatrixItems.Any(a => a.TherapistId == request.IdAsLong && a.IsOwner == request.IsOwner))
                .OrderBy(p => p.Id);

            Mapper.Map(plans, responseList);

            return
                Ok(new ApiListResponse<BriefPlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(TherapistEpisodeListRequest request)
        {
            var responseList = new List<EpisodeDto>();
            var episodes = EpisodeUnitOfWork.GetFilteredEpisodes(request.IdAsInt, state: request.State.ToString());

            Mapper.Map(episodes, responseList);

            return
                Ok(new ApiListResponse<EpisodeDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(TherapistSharedPlanListRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "TherapistId");

            var sharedPlans = PlanUnitOfWork.GetSharedPlansByTherapist(request.IdAsInt);

            if (request.PlanId > 0)
                sharedPlans = sharedPlans.Where(p => p.Id == request.PlanId);

            var responseList = new List<TherapistSharedPlanDto>();

            Mapper.Map(sharedPlans.ToList().SelectMany(s => s.ClinicPlanMatrixItems), responseList);

            return Ok(new ApiListResponse<TherapistSharedPlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(TherapistSharedExerciseListRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "TherapistId");

            var sharedExercises = ExerciseUnitOfWork.GetSharedExercisesByTherapist(request.IdAsInt);

            if (request.ExerciseId > 0)
                sharedExercises = sharedExercises.Where(p => p.Id == request.ExerciseId);

            var responseList = new List<TherapistSharedExerciseDto>();

            Mapper.Map(sharedExercises.ToList().SelectMany(s => s.ClinicExerciseMatrixItems), responseList);

            return Ok(new ApiListResponse<TherapistSharedExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        public object Put(UpdateTherapistSharedPlanRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "TherapistId");

            if (request.SharedPlans.Any())
                PlanUnitOfWork.UpdateSharedPlans(Mapper.Map<IEnumerable<ClinicPlanMatrixItem>>(request.SharedPlans));

            return Ok();
        }

        public object Put(UpdateTherapistSharedExerciseRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "TherapistId");

            if (request.SharedExercises.Any())
                ExerciseUnitOfWork.UpdateSharedExercises(Mapper.Map<IEnumerable<ClinicExerciseMatrixItem>>(request.SharedExercises));

            return Ok();
        }

        #endregion

    }
}
