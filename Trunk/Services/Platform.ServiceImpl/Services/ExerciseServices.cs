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

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods
       

        public object Get(BriefExerciseListRequest request)
        {
            var responseList = new List<BriefExerciseDto>();
            var exercises = ResearchUnitOfWork.ExerciseRepo.GetAll().OrderBy(p => p.Id);

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

        public object Get(TherapistExerciseListRequest request)
        {
            var responseList = new List<BriefExerciseDto>();
            var exercises = ResearchUnitOfWork.ExerciseRepo.GetAll().Where(p => p.TherapistExerciseMatrixItems.Any(w => w.TherapistId == request.IdAsLong));

            Mapper.Map(exercises, responseList);

            return
                Ok(new ApiListResponse<BriefExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Get(ExerciseRequest request)
        {
            var exerciseQuery = ResearchUnitOfWork.ExerciseRepo.GetAll();

            var exercise = request.IdAsInt > 0
                ? exerciseQuery.FirstOrDefault(p => p.Id == request.IdAsInt)
                : exerciseQuery.FirstOrDefault(p => p.PublishDetail.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));

            return Ok(new ApiResponse<ExerciseDto>() {Response = Mapper.Map<ExerciseDto>(exercise)});

        }

        public object Post(CreateExerciseRequest request)
        {
            Check.Argument.IsNotNull(request, "ExerciseDto");

            var exercise = Mapper.Map<Exercise>(request);

            ResearchUnitOfWork.ExerciseRepo.Add(exercise);
            ResearchUnitOfWork.Commit();

            request.Id = exercise.Id;

            return Ok(new ApiResponse<ExerciseDto>(request));

        }

        public object Put(UpdateExerciseRequest request)
        {
            Check.Argument.IsNotNull(request, "ExerciseDto");

            ResearchUnitOfWork.UpdateExercise(Mapper.Map<Exercise>(request));

            return Ok(new ApiResponse<ExerciseDto>(request));
        }

        public object Patch(PublishExerciseRequest request)
        {
            var publishDetail = Mapper.Map<ExercisePublishDetail>(request);

            ResearchUnitOfWork.ExercisePublishDetailRepo.Update(publishDetail);
            ResearchUnitOfWork.Commit();

            return Ok();
        }

        #endregion
    }
}
