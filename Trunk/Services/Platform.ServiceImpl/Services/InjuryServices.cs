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
     

        public object Get(BriefInjuryListRequest request)
        {
            var responseList = new List<BriefInjuryDto>();

            var injuries =
                ResearchUnitOfWork.InjuryRepo.GetAll().OrderBy(p => p.Id);

            var predicate = PredicateBuilder.True<Injury>();

            if (request.ClinicId > 0 && request.IsPublic != null)
                predicate = predicate.And(
                    p =>
                        p.ClinicInjuryMatrixItems.Any(
                            f => f.IsPublic == request.IsPublic && f.ClinicId == request.ClinicId));
            else if (request.ClinicId > 0)
                predicate = predicate.And(
                    p =>
                        p.ClinicInjuryMatrixItems.Any(f => f.ClinicId == request.ClinicId));
            else if (request.IsPublic != null)
                predicate = predicate.And(
                    p =>
                        p.ClinicInjuryMatrixItems.Any(f => f.IsPublic == request.IsPublic));

            Mapper.Map(injuries.AsExpandable().Where(predicate), responseList);

            return
                Ok(new ApiListResponse<BriefInjuryDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Get(InjuryRequest request)
        {
            var injuryQuery = ResearchUnitOfWork.InjuryRepo.GetInjuryDetails();
            
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
