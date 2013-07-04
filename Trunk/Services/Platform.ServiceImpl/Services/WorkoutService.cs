using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.DataAccess.UnitOfWork;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class WorkoutService : LoggingRestServiceBase<WorkoutRequest, ApiResponse<WorkoutDto>>
    {
        #region Properties

        public IWorkoutUnitOfWork WorkoutUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(WorkoutRequest request)
        {
            var workoutEntity = WorkoutUnitOfWork.WorkoutRepo.GetFullWorkoutGraphById(request.IdAsInt);
            var exerciseIds = workoutEntity.WorkoutExerciseMatrixItems.Select(s => s.ExerciseId);
            var exerciseEntities =
                WorkoutUnitOfWork.ExerciseRepo.GetAll()
                                 .Where(p => exerciseIds.Contains(p.Id)).ToList();

            var workoutDto = Mapper.Map<WorkoutDto>(workoutEntity);
            var exerciseDtos = new List<ExerciseDto>();
            Mapper.Map(exerciseEntities, exerciseDtos);

            if(exerciseEntities.Any())
                workoutDto.exercises = exerciseDtos.ToArray();

            return Ok(new ApiResponse<WorkoutDto>()
            {
                Resource = workoutDto
            });
        }

        #endregion

    }
}
