using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PlanListService : LoggingRestServiceBase<PlanListRequest, ListResponse<PlanDto, BasicSortBy>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(PlanListRequest request)
        {
            //TODO: TON O DATA.... Need to refactor at some point to not pull entire graph...
            //just get it working for now, maybe put a bool in request 

            var responseList = new List<PlanDto>();

            var plans = ResearchUnitOfWork.PlanRepo.GetAll(new[]
                    {
                     "PlanExerciseMatrixItems",
                     "PlanExerciseMatrixItems.Exercise",
                     "PlanBodyRegionMatrixItems",
                     "PlanBodyRegionMatrixItems.BodyRegion",
                    });

            var exerciseIds = plans.SelectMany(p => p.PlanExerciseMatrixItems).Select(s => s.ExerciseId);
            var exerciseEntities =
                ResearchUnitOfWork.ExerciseRepo.GetAll(new[] { "ExerciseEquipmentMatrixItems", "ExerciseEquipmentMatrixItems.Equipment", 
                                                               "ExerciseVideoMatrixItems", "ExerciseVideoMatrixItems.Video" })
                                 .Where(p => exerciseIds.Contains(p.Id)).ToList();

            foreach (var planExercise in plans.SelectMany(p => p.PlanExerciseMatrixItems))
                planExercise.Exercise = exerciseEntities.Single(p => p.Id == planExercise.ExerciseId);

            Mapper.Map(plans, responseList);

            return
                Ok(new ListResponse<PlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class PlanService : LoggingRestServiceBase<PlanRequest, ApiResponse<PlanDto>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(PlanRequest request)
        {
            var planEntity = ResearchUnitOfWork.PlanRepo.GetFullPlanGraphById(request.IdAsInt);
            var exerciseIds = planEntity.PlanExerciseMatrixItems.Select(s => s.ExerciseId);
            var exerciseEntities =
                ResearchUnitOfWork.ExerciseRepo.GetAll()
                                 .Where(p => exerciseIds.Contains(p.Id)).ToList();

            var planDto = Mapper.Map<PlanDto>(planEntity);
            var exerciseDtos = new List<ExerciseDto>();
            Mapper.Map(exerciseEntities, exerciseDtos);

            if(exerciseEntities.Any())
                planDto.exercises = exerciseDtos.ToArray();

            return Ok(new ApiResponse<PlanDto>()
            {
                Resource = planDto
            });
        }

        public override object OnPost(PlanRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "PlanDto");

            var plan = Mapper.Map<Plan>(request.Resource);

            ResearchUnitOfWork.PlanRepo.Add(plan);
            ResearchUnitOfWork.Commit();

            request.Resource.id = plan.Id;

            return Ok(new ApiResponse<PlanDto>(request.Resource));
        }

        public override object OnPut(PlanRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "PlanDto");

            ResearchUnitOfWork.UpdatePlan(Mapper.Map<Plan>(request.Resource));

            return Ok(new ApiResponse<PlanDto>(request.Resource));
        }

        #endregion

    }
}
