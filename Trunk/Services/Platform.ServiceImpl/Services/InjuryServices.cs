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
    public class InjuryListService : LoggingRestServiceBase<InjuryListRequest, ListResponse<InjuryDto, BasicSortBy>>
    {

        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(InjuryListRequest request)
        {
            var responseList = new List<InjuryDto>();

            var results =
                ResearchUnitOfWork.InjuryRepo.GetAll(new[]
                    {
                        "InjuryPlanMatrixItems", 
                        "InjuryPlanMatrixItems.Plan", 
                        "InjurySignMatrixItems",
                        "InjurySignMatrixItems.Sign", 
                        "InjuryCauseMatrixItems", 
                        "InjuryCauseMatrixItems.Cause",
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
                    }).OrderBy(p => p.CommonName);

            Mapper.Map(results, responseList);
            
            return
                Ok(new ListResponse<InjuryDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class InjuryService : LoggingRestServiceBase<InjuryRequest, ApiResponse<InjuryDto>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(InjuryRequest request)
        {
            var injury = request.IdAsInt > 0
                   ? ResearchUnitOfWork.InjuryRepo.GetById(request.IdAsInt)
                   : ResearchUnitOfWork.InjuryRepo.GetAll(new[]
                    {
                        "InjuryPlanMatrixItems", "InjuryPlanMatrixItems.Plan", "InjurySignMatrixItems",
                        "InjurySignMatrixItems.Sign", "InjuryCauseMatrixItems", "InjuryCauseMatrixItems.Cause",
                        "InjuryBodyRegionMatrixItems", "InjuryBodyRegionMatrixItems.BodyRegion","InjuryPlanMatrixItems.Plan.PlanCategoryMatrixItems"
                    }).FirstOrDefault(p => p.PageName.Equals(request.Id, StringComparison.OrdinalIgnoreCase));

            return Ok(new ApiResponse<InjuryDto>() { Resource = Mapper.Map<InjuryDto>(injury) });
        }

        public override object OnPost(InjuryRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "InjuryDto");

            var injury = Mapper.Map<Injury>(request.Resource);

            ResearchUnitOfWork.MapSymptomMatrixItems(injury);
            ResearchUnitOfWork.InjuryRepo.Add(injury);
            ResearchUnitOfWork.Commit();

            request.Resource.id = injury.Id;

            return Ok(new ApiResponse<InjuryDto>(request.Resource));

        }

        public override object OnPut(InjuryRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "InjuryDto");
            var injury = Mapper.Map<Injury>(request.Resource);

            ResearchUnitOfWork.MapSymptomMatrixItems(injury);
            ResearchUnitOfWork.UpdateInjury(injury);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<InjuryDto>(request.Resource));
        }

        #endregion
    }
}
