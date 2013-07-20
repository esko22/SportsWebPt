using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class ExerciseListService : LoggingRestServiceBase<ExecerciseListRequest, ListResponse<ExerciseDto, BasicSortBy>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(ExecerciseListRequest request)
        {
            var responseList = new List<ExerciseDto>();
            Mapper.Map(
                ResearchUnitOfWork.ExerciseRepo.GetAll(new[]
                    {
                        "ExerciseEquipmentMatrixItems.Equipment", "ExerciseVideoMatrixItems.Video",
                        "ExerciseBodyRegionMatrixItems.BodyRegion"
                    }), responseList);

            return
                Ok(new ListResponse<ExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class ExerciseService : LoggingRestServiceBase<ExecerciseRequest, ApiResponse<ExerciseDto>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(ExecerciseRequest request)
        {
            var exercise = request.IdAsInt > 0
                               ? ResearchUnitOfWork.ExerciseRepo.GetById(request.IdAsInt)
                               : ResearchUnitOfWork.ExerciseRepo.GetAll(new[]
                    {
                        "ExerciseEquipmentMatrixItems.Equipment", "ExerciseVideoMatrixItems.Video",
                        "ExerciseBodyRegionMatrixItems.BodyRegion"
                    }).FirstOrDefault(p => p.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));
            return Ok(new ApiResponse<ExerciseDto>() {Resource = Mapper.Map<ExerciseDto>(exercise)});

        }

        public override object OnPost(ExecerciseRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "ExerciseDto");

            var exercise = Mapper.Map<Exercise>(request.Resource);

            ResearchUnitOfWork.ExerciseRepo.Add(exercise);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<ExerciseDto>(request.Resource));

        }

        public override object OnPut(ExecerciseRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "ExerciseDto");

            ResearchUnitOfWork.UpdateExercise(Mapper.Map<Exercise>(request.Resource));

            return Ok(new ApiResponse<ExerciseDto>(request.Resource));
        }

        #endregion
    }
}
