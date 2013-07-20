using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.DataAccess.UnitOfWork;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PlanListService : LoggingRestServiceBase<PlanListRequest, ListResponse<PlanDto, BasicSortBy>>
    {
        #region Properties

        public IPlanUnitOfWork PlanUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(PlanListRequest request)
        {
            var responseList = new List<PlanDto>();
            Mapper.Map(
                PlanUnitOfWork.PlanRepo.GetAll(new[]
                    {""
                       
                    }), responseList);

            return
                Ok(new ListResponse<PlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class PlanService : LoggingRestServiceBase<PlanRequest, ApiResponse<PlanDto>>
    {
        #region Properties

        public IPlanUnitOfWork PlanUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(PlanRequest request)
        {
            var planEntity = PlanUnitOfWork.PlanRepo.GetFullPlanGraphById(request.IdAsInt);
            var exerciseIds = planEntity.PlanExerciseMatrixItems.Select(s => s.ExerciseId);
            var exerciseEntities =
                PlanUnitOfWork.ExerciseRepo.GetAll()
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

        #endregion

    }
}
