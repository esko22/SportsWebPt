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

    public class PlanService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(PlanListRequest request)
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
                     "PlanCategoryMatrixItems"
                    }).OrderBy(p => p.Id);



            var exerciseIds = plans.SelectMany(p => p.PlanExerciseMatrixItems).Select(s => s.ExerciseId);
            var exerciseEntities =
                ResearchUnitOfWork.ExerciseRepo.GetAll(new[] { "ExerciseEquipmentMatrixItems", "ExerciseEquipmentMatrixItems.Equipment", 
                                                               "ExerciseVideoMatrixItems", "ExerciseVideoMatrixItems.Video", "ExerciseVideoMatrixItems.Video.VideoCategoryMatrixItems" })
                                 .Where(p => exerciseIds.Contains(p.Id)).ToList();

            foreach (var plan in plans)
                plan.PlanExerciseMatrixItems = plan.PlanExerciseMatrixItems.OrderBy(p => p.Order).ToList();

            foreach (var planExercise in plans.SelectMany(p => p.PlanExerciseMatrixItems))
                planExercise.Exercise = exerciseEntities.Single(p => p.Id == planExercise.ExerciseId);

            Mapper.Map(plans, responseList);

            return
                Ok(new ApiListResponse<PlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        public object Get(BriefPlanListRequest request)
        {
            //TODO: TON O DATA.... Need to refactor at some point to not pull entire graph...
            //just get it working for now, maybe put a bool in request 

            var responseList = new List<BriefPlanDto>();

            var clinicPlans = ResearchUnitOfWork.ClinicPlanRepo.GetAll(new[]
                    {
                     "Plan.PublishDetail",
                     "Plan.PlanExerciseMatrixItems",
                     "Plan.PlanExerciseMatrixItems.Exercise",
                     "Plan.PlanBodyRegionMatrixItems",
                     "Plan.PlanBodyRegionMatrixItems.BodyRegion",
                     "Plan.PlanCategoryMatrixItems"
                    }).Where(p => p.IsPublic && p.ClinicId == 2).OrderBy(p => p.PlanId);


            var exerciseIds =  clinicPlans.SelectMany(p => p.Plan.PlanExerciseMatrixItems).Select(s => s.ExerciseId);
            var exerciseEntities =
                ResearchUnitOfWork.ExerciseRepo.GetAll(new[] { "ExerciseEquipmentMatrixItems", "ExerciseEquipmentMatrixItems.Equipment" })
                                 .Where(p => exerciseIds.Contains(p.Id)).ToList();

            Mapper.Map(clinicPlans, responseList);

            return
                Ok(new ApiListResponse<BriefPlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }




        public object Get(PlanRequest request)
        {
            var planEntity = request.IdAsInt > 0
                    ? ResearchUnitOfWork.PlanRepo.GetFullPlanGraphById(request.IdAsInt)
                    : ResearchUnitOfWork.PlanRepo.GetAll(new[]
                                {
                                    "PlanExerciseMatrixItems",
                                    "PlanExerciseMatrixItems.Exercise",
                                    "PlanBodyRegionMatrixItems",
                                    "PlanBodyRegionMatrixItems.BodyRegion",
                                    "PlanCategoryMatrixItems",
                                    "PublishDetail"
                                }).FirstOrDefault(p => p.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));

            if (planEntity == null)
                return NotFound("Plan Not Found");

            planEntity.PlanExerciseMatrixItems = planEntity.PlanExerciseMatrixItems.OrderBy(p => p.Order).ToList();
            var exerciseIds = planEntity.PlanExerciseMatrixItems.Select(s => s.ExerciseId);
            var exerciseEntities =
                ResearchUnitOfWork.ExerciseRepo.GetAll(new[] { "ExerciseEquipmentMatrixItems", "ExerciseEquipmentMatrixItems.Equipment", 
                                                               "ExerciseVideoMatrixItems", "ExerciseVideoMatrixItems.Video", "ExerciseVideoMatrixItems.Video.VideoCategoryMatrixItems" })
                                 .Where(p => exerciseIds.Contains(p.Id)).ToList();

            foreach (var planExercise in planEntity.PlanExerciseMatrixItems)
                planExercise.Exercise = exerciseEntities.Single(p => p.Id == planExercise.ExerciseId);

            var planDto = Mapper.Map<PlanDto>(planEntity);

            return Ok(new ApiResponse<PlanDto>()
            {
                Response = planDto
            });
        }

        public object Post(CreatePlanRequest request)
        {
            Check.Argument.IsNotNull(request, "PlanDto");

            var plan = Mapper.Map<Plan>(request);

            ResearchUnitOfWork.PlanRepo.Add(plan);
            ResearchUnitOfWork.Commit();

            request.Id = plan.Id;

            return Ok(new ApiResponse<PlanDto>(request));
        }

        public object Put(UpdatePlanRequest request)
        {
            Check.Argument.IsNotNull(request, "PlanDto");

            ResearchUnitOfWork.UpdatePlan(Mapper.Map<Plan>(request));

            return Ok(new ApiResponse<PlanDto>(request));
        }

        #endregion

    }
}
