﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        public IPlanUnitOfWork PlanUnitOfWork { get; set; }

        #endregion

        #region Methods
     

        public object Get(BriefPlanListRequest request)
        {
            var responseList = new List<BriefPlanDto>();

            var plans = PlanUnitOfWork.PlanRepo.GetPlanDetails().OrderBy(p => p.Id);
            var predicate = PredicateBuilder.True<Plan>();

            if (request.ClinicId > 0 && request.IsPublic != null)
                predicate = predicate.And(
                    p =>
                        p.ClinicPlanMatrixItems.Any(
                            f => f.IsActive && f.ClinicId == request.ClinicId) && p.PublishDetail.Visible == request.IsPublic);
            else if (request.ClinicId > 0)
                predicate = predicate.And(
                    p =>
                        p.ClinicPlanMatrixItems.Any(f => f.ClinicId == request.ClinicId && f.IsActive));
            else if (request.IsPublic != null)
                predicate = predicate.And(
                    p =>
                        p.ClinicPlanMatrixItems.Any(f => f.IsActive) && p.PublishDetail.Visible == request.IsPublic);
            
            Mapper.Map(plans.AsExpandable().Where(predicate), responseList);

            return
                Ok(new ApiListResponse<BriefPlanDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }



        public object Get(PlanRequest request)
        {
            var planEntity = request.IdAsInt > 0
                    ? PlanUnitOfWork.PlanRepo.GetPlanDetails().SingleOrDefault(p => p.Id == request.IdAsInt)
                    : PlanUnitOfWork.PlanRepo.GetPlanDetails().SingleOrDefault(p => p.PublishDetail.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));

            if (planEntity == null)
                return NotFound("Plan Not Found");

            //TODO: this seems to be an issue in the MySql Connect, will not allow l3 joins on equipment and video
            planEntity.PlanExerciseMatrixItems = planEntity.PlanExerciseMatrixItems.OrderBy(p => p.Order).ToList();
            var exerciseIds = planEntity.PlanExerciseMatrixItems.Select(s => s.ExerciseId);
            var exerciseEntities =
                PlanUnitOfWork.ExerciseRepo.GetExerciseDetails()
                                 .Where(p => exerciseIds.Contains(p.Id)).ToList();

            foreach (var planExercise in planEntity.PlanExerciseMatrixItems)
                planExercise.Exercise = exerciseEntities.Single(p => p.Id == planExercise.ExerciseId);


            if (!String.IsNullOrEmpty(request.RequestorId))
            {
                var requestorId = new Guid(request.RequestorId);
                var theraList = planEntity.TherapistPlanMatrixItems.ToList();
                theraList.RemoveAll(r => !r.TherapistId.Equals(requestorId));
                planEntity.TherapistPlanMatrixItems = theraList;
            }


            var planDto = Mapper.Map<PlanDto>(planEntity);

            return Ok(new ApiResponse<PlanDto>()
            {
                Response = planDto
            });
        }

        public object Post(CreatePlanRequest request)
        {
            Check.Argument.IsNotNull(request, "PlanDto");
            Check.Argument.IsNotNullOrEmpty(request.TherapistId, "TherapistId");

            var plan = Mapper.Map<Plan>(request);
            plan.TherapistPlanMatrixItems = new List<TherapistPlanMatrixItem>() {new TherapistPlanMatrixItem() { TherapistId = new Guid(request.TherapistId), IsOwner = true, IsActive = true }};

            PlanUnitOfWork.PlanRepo.Add(plan);
            PlanUnitOfWork.Commit();

            request.Id = plan.Id;

            return Ok(new ApiResponse<PlanDto>(request));
        }

        public object Patch(PublishPlanRequest request)
        {
            var publishDetail = PlanUnitOfWork.PlanPublishRepo.GetById(request.IdAsInt);

            if (publishDetail == null)
            {
                publishDetail = Mapper.Map<PlanPublishDetail>(request);
                PlanUnitOfWork.PlanPublishRepo.Add(publishDetail);
            }
            else
            {
                publishDetail.Tags = request.Tags;
                publishDetail.PageName = request.PageName;
                publishDetail.Visible = request.Visible;

                PlanUnitOfWork.PlanPublishRepo.Update(publishDetail);
            }

            PlanUnitOfWork.Commit();

            return Ok();
        }

        public object Put(UpdatePlanRequest request)
        {
            Check.Argument.IsNotNull(request, "PlanDto");

            PlanUnitOfWork.UpdatePlan(Mapper.Map<Plan>(request));

            return Ok(new ApiResponse<PlanDto>(request));
        }

        #endregion

    }
}
