using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class ExerciseService : RestService
    {
        #region Properties

        public IExerciseUnitOfWork ExerciseUnitOfWork { get; set; }

        #endregion

        #region Methods
       

        public object Get(BriefExerciseListRequest request)
        {
            var responseList = new List<BriefExerciseDto>();
            var exercises = ExerciseUnitOfWork.ExerciseRepo.GetExerciseDetails().OrderBy(p => p.Id);

            var predicate = PredicateBuilder.True<Exercise>();

            if (request.ClinicId > 0 && request.IsPublic != null)
                predicate = predicate.And(
                    p =>
                        p.ClinicExerciseMatrixItems.Any(
                            f => f.IsActive && f.ClinicId == request.ClinicId) && p.PublishDetail.Visible == request.IsPublic);
            else if (request.ClinicId > 0)
                predicate = predicate.And(
                    p =>
                        p.ClinicExerciseMatrixItems.Any(f => f.ClinicId == request.ClinicId && f.IsActive));
            else if (request.IsPublic != null)
                predicate = predicate.And(
                    p =>
                        p.ClinicExerciseMatrixItems.Any(f => f.IsActive) && p.PublishDetail.Visible == request.IsPublic);

            Mapper.Map(exercises.AsExpandable().Where(predicate), responseList);

            return
                Ok(new ApiListResponse<BriefExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Get(ExerciseRequest request)
        {
            var exerciseQuery = ExerciseUnitOfWork.ExerciseRepo.GetExerciseDetails();

            var exercise = request.IdAsInt > 0
                ? exerciseQuery.FirstOrDefault(p => p.Id == request.IdAsInt)
                : exerciseQuery.FirstOrDefault(p => p.PublishDetail.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));

            return Ok(new ApiResponse<ExerciseDto>() {Response = Mapper.Map<ExerciseDto>(exercise)});

        }

        public object Post(CreateExerciseRequest request)
        {
            Check.Argument.IsNotNull(request, "ExerciseDto");

            var exercise = Mapper.Map<Exercise>(request);
            exercise.TherapistExerciseMatrixItems = new List<TherapistExerciseMatrixItem>() { new TherapistExerciseMatrixItem() { TherapistId = new Guid(request.TherapistId), IsOwner = true, IsActive = true } };

            ExerciseUnitOfWork.ExerciseRepo.Add(exercise);
            ExerciseUnitOfWork.Commit();

            request.Id = exercise.Id;

            return Ok(new ApiResponse<ExerciseDto>(request));

        }

        public object Put(UpdateExerciseRequest request)
        {
            Check.Argument.IsNotNull(request, "ExerciseDto");

            ExerciseUnitOfWork.UpdateExercise(Mapper.Map<Exercise>(request));

            return Ok(new ApiResponse<ExerciseDto>(request));
        }

        public object Patch(PublishExerciseRequest request)
        {
            var publishDetail = ExerciseUnitOfWork.ExercisePublishDetailRepo.GetById(request.IdAsInt);

            if (publishDetail == null)
            {
                publishDetail = Mapper.Map<ExercisePublishDetail>(request);
                ExerciseUnitOfWork.ExercisePublishDetailRepo.Add(publishDetail);
            }
            else
            {
                publishDetail.PageName = request.PageName;
                publishDetail.Tags = request.Tags;
                publishDetail.Visible = request.Visible;
                ExerciseUnitOfWork.ExercisePublishDetailRepo.Update(publishDetail);
            }


            ExerciseUnitOfWork.Commit();

            return Ok();
        }

        #endregion
    }
}
