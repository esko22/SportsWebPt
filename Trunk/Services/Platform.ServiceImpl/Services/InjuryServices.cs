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

    public class InjuryService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(InjuryListRequest request)
        {
            var responseList = new List<InjuryDto>();

            //TODO: this needs to moved into the UOW... Nav paths do not belong in service layer
            var results =
                ResearchUnitOfWork.InjuryRepo.GetAll(new[]
                    {
                        "InjuryPlanMatrixItems", 
                        "InjuryPlanMatrixItems.Plan", 
                        "InjurySignMatrixItems",
                        "InjurySignMatrixItems.Sign", 
                        "InjurySignMatrixItems.Sign.Filter", 
                        "InjuryCauseMatrixItems", 
                        "InjuryCauseMatrixItems.Cause",
                        "InjuryTreatmentMatrixItems", 
                        "InjuryTreatmentMatrixItems.Treatment",
                        "InjuryPrognosisMatrixItems", 
                        "InjuryPrognosisMatrixItems.Prognosis",
                        "InjuryBodyRegionMatrixItems", 
                        "InjuryBodyRegionMatrixItems.BodyRegion",
                        "InjurySymptomMatrixItems",
                        "InjurySymptomMatrixItems.SymptomMatrixItem",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.Symptom",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.Symptom.RenderType",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem.BodyPart",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem.SkeletonArea",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem.SkeletonArea.Side",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem.SkeletonArea.Orientation",
                        "InjuryPlanMatrixItems.Plan.PlanCategoryMatrixItems"
                    }).OrderBy(p => p.Id);

            Mapper.Map(results, responseList);

            return
                Ok(new ApiListResponse<InjuryDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(BriefInjuryListRequest request)
        {
            var responseList = new List<BriefInjuryDto>();

            //TODO: this needs to moved into the UOW... Nav paths do not belong in service layer
            var results =
                ResearchUnitOfWork.ClinicInjuryRepo.GetAll(new[]
                    {
                        "Injury",
                        "Injury.PublishDetail",
                        "Injury.InjurySignMatrixItems",
                        "Injury.InjurySignMatrixItems.Sign", 
                        "Injury.InjurySignMatrixItems.Sign.Filter", 
                        "Injury.InjuryBodyRegionMatrixItems", 
                        "Injury.InjuryBodyRegionMatrixItems.BodyRegion",
                    }).Where(p => p.IsPublic && p.ClinicId == PlatformServiceConfiguration.Instance.ClinicId).OrderBy(p => p.Id);

            //projecting out here since projection from join loses includes
            var injuries = new List<Injury>();
            results.ForEach(c => injuries.Add(c.Injury));

            Mapper.Map(injuries, responseList);

            return
                Ok(new ApiListResponse<BriefInjuryDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Get(InjuryRequest request)
        {
            var injuryQuery = ResearchUnitOfWork.InjuryRepo.GetAll(new[]
                    {
                        "InjuryPlanMatrixItems", "InjuryPlanMatrixItems.Plan", "InjurySignMatrixItems",
                        "InjurySignMatrixItems.Sign", "InjuryCauseMatrixItems", "InjuryCauseMatrixItems.Cause","InjuryTreatmentMatrixItems", "InjuryPrognosisMatrixItems", 
                        "InjuryPrognosisMatrixItems.Prognosis", "InjuryTreatmentMatrixItems.Treatment","InjuryBodyRegionMatrixItems", "InjuryBodyRegionMatrixItems.BodyRegion",
                        "InjuryPlanMatrixItems.Plan.PlanCategoryMatrixItems", "PublishDetail"
                    });
            
            var injury = request.IdAsInt > 0
                ? injuryQuery.FirstOrDefault(p => p.Id == request.IdAsInt)
                : injuryQuery.FirstOrDefault(p => p.PublishDetail.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));

            return Ok(new ApiResponse<InjuryDto>() { Response = Mapper.Map<InjuryDto>(injury) });
        }

        public object Post(CreateInjuryRequest request)
        {
            Check.Argument.IsNotNull(request, "InjuryDto");

            var injury = Mapper.Map<Injury>(request);

            ResearchUnitOfWork.MapSymptomMatrixItems(injury);
            ResearchUnitOfWork.InjuryRepo.Add(injury);
            ResearchUnitOfWork.Commit();

            request.Id = injury.Id;

            return Ok(new ApiResponse<InjuryDto>(request));
        }

        public object Put(UpdateInjuryRequest request)
        {
            Check.Argument.IsNotNull(request, "InjuryDto");
            var injury = Mapper.Map<Injury>(request);

            ResearchUnitOfWork.MapSymptomMatrixItems(injury);
            ResearchUnitOfWork.UpdateInjury(injury);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<InjuryDto>(request));
        }

        #endregion
    }
}
