﻿using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class DiagnosisReportService : RestService
    {
        #region Properties

        public IDiffDiagUnitOfWork DiffDiagUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(DiagnosisReportRequest request)
        {
            var differentialDiagEntity = DiffDiagUnitOfWork.DiffDiagRepo.GetById(request.IdAsInt);
            var diagnosisReportDto = Mapper.Map<DiagnosisReportDto>(differentialDiagEntity);

            var distinctPotentialInjuries =
                DiffDiagUnitOfWork.SymptomResponseRepo.GetAll()
                                  .Where(p => p.DifferentialDiagnosisId == request.IdAsInt && p.GivenResponse > 0)
                                  .Join(DiffDiagUnitOfWork.InjurySymptomMatrixItemRepo.GetAll(),
                                        sd => sd.SymptomMatrixItemId, ismi => ismi.SymptomMatrixItemId,
                                        (detail, item) => new {detail, item}).Where(isimisd => isimisd.detail.GivenResponse >= isimisd.item.ThresholdValue)
                                  .Join(DiffDiagUnitOfWork.InjuryRepo.GetAll(), ismisd => ismisd.item.InjuryId,
                                        injury => injury.Id, (ismisd, injury) => injury).Distinct();

            var potentialInjuryDtos = new List<PotentialInjuryDto>();
            //TODO: this is tuurible... have to go back to get inlcudes cuz I cant figure out how to cleanly get it above yet
            var potentialInjuries =
                DiffDiagUnitOfWork.InjuryRepo.GetAll(new[] { "InjuryPlanMatrixItems", "InjuryPlanMatrixItems.Plan", "InjurySignMatrixItems", "InjuryPlanMatrixItems.Plan.PlanCategoryMatrixItems",
                                                             "InjurySignMatrixItems.Sign", "InjuryCauseMatrixItems", "InjuryCauseMatrixItems.Cause" })
                                  .Where(p => distinctPotentialInjuries.Contains(p));
            
            Mapper.Map(potentialInjuries, potentialInjuryDtos);

            var givenSymptoms =
                DiffDiagUnitOfWork.SymptomResponseRepo.GetAll(new[]
                    {
                        "SymptomMatrixItem", "SymptomMatrixItem.InjurySymptomMatrixItems", "SymptomMatrixItem.Symptom",
                        "SymptomMatrixItem.BodyPartMatrixItem", "SymptomMatrixItem.BodyPartMatrixItem.BodyPart"
                    })
                                  .Where(p => p.DifferentialDiagnosisId == request.IdAsInt && p.GivenResponse > 0);

            //TODO: this whole service is a fucking mess... gotta be a better way
            foreach (var potentialInjuryDto in potentialInjuryDtos)
            {
                var givenSymptomDtos = new List<PotentialSymptomDto>();

                givenSymptoms.ForEach(
                    p =>
                    p.SymptomMatrixItem.InjurySymptomMatrixItems.Where(i => i.InjuryId == potentialInjuryDto.Id)
                     .ForEach(i => givenSymptomDtos.Add(Mapper.Map<PotentialSymptomDto>(p))));

                potentialInjuryDto.GivenSymptoms = givenSymptomDtos.ToArray();
            }



            if (potentialInjuryDtos.Count > 0)
                diagnosisReportDto.PotentialInjuries = potentialInjuryDtos.ToArray();

            return Ok(new ApiResponse<DiagnosisReportDto>()
            {
                Response = diagnosisReportDto
            });
        }


        public object Post(CreateDiagnosisReportRequest request)
        {
            var differentialDiagEntity = Mapper.Map<DifferentialDiagnosis>(request);
            DiffDiagUnitOfWork.DiffDiagRepo.Add(differentialDiagEntity);
            DiffDiagUnitOfWork.Commit();

            return Ok(new ApiResponse<DifferentialDiagnosisDto>()
            {
                Response = Mapper.Map<DifferentialDiagnosisDto>(differentialDiagEntity)
            });
        }


        #endregion

    }
}
