using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
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

        public object Get(ExerciseListRequest request)
        {
            var responseList = new List<ExerciseDto>();
            Mapper.Map(
                ResearchUnitOfWork.ExerciseRepo.GetAll(new[]
                    {
                        "ExerciseEquipmentMatrixItems.Equipment", "ExerciseVideoMatrixItems.Video",
                        "ExerciseBodyRegionMatrixItems.BodyRegion", "ExerciseBodyPartMatrixItems.BodyPart","ExerciseCategoryMatrixItems", "ExerciseVideoMatrixItems.Video.VideoCategoryMatrixItems"
                    }).OrderBy(p => p.Id), responseList);

            return
                Ok(new ApiListResponse<ExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        public object Get(BriefExerciseListRequest request)
        {
            var responseList = new List<BriefExerciseDto>();
            Mapper.Map(
                ResearchUnitOfWork.ExerciseRepo.GetAll(new[]
                    {
                        "ExerciseEquipmentMatrixItems.Equipment", "ExerciseBodyRegionMatrixItems.BodyRegion", "ExerciseBodyPartMatrixItems.BodyPart",
                        "ExerciseCategoryMatrixItems"
                    }).OrderBy(p => p.Id), responseList);

            return
                Ok(new ApiListResponse<BriefExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }


        public object Get(ExerciseRequest request)
        {
            var exerciseQuery = ResearchUnitOfWork.ExerciseRepo.GetAll(new[]
            {
                "ExerciseEquipmentMatrixItems.Equipment", "ExerciseVideoMatrixItems.Video",
                "ExerciseBodyRegionMatrixItems.BodyRegion", "ExerciseBodyPartMatrixItems.BodyPart",
                "ExerciseCategoryMatrixItems", "ExerciseVideoMatrixItems.Video.VideoCategoryMatrixItems", "PublishDetail"
            });

            var exercise = request.IdAsInt > 0
                ? exerciseQuery.FirstOrDefault(p => p.Id == request.IdAsInt)
                : exerciseQuery.FirstOrDefault(p => p.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));

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

        #endregion
    }
}
