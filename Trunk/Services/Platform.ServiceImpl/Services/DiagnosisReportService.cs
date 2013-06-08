using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using ServiceStack.ServiceInterface.ServiceModel;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class DiagnosisReportService : LoggingRestServiceBase<DiagnosisReportRequest, ApiResponse<DiagnosisReportDto>>
    {
        #region Properties

        public IDiffDiagUnitOfWork DiffDiagUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(DiagnosisReportRequest request)
        {
            var differentialDiagEntity = DiffDiagUnitOfWork.DiffDiagRepo.GetById(request.IdAsInt);
            var diagnosisReportDto = Mapper.Map<DiagnosisReportDto>(differentialDiagEntity);

            var distinctPotentialInjuries =
                DiffDiagUnitOfWork.SymptomResponseRepo.GetAll()
                                  .Where(p => p.DifferentialDiagnosisId == request.IdAsInt && p.GivenResponse > 0)
                                  .Join(DiffDiagUnitOfWork.InjurySymptomMatrixItemRepo.GetAll(),
                                        sd => sd.SymptomMatrixItemId, ismi => ismi.SymptomMatrixItemId,
                                        (detail, item) => new {detail, item}).Where(isimisd => isimisd.detail.GivenResponse > isimisd.item.ThresholdValue)
                                  .Join(DiffDiagUnitOfWork.InjuryRepo.GetAll(), ismisd => ismisd.item.InjuryId,
                                        injury => injury.Id, (ismisd, injury) => injury).Distinct();

            var potentialInjuryDtos = new List<PotentialInjuryDto>();
            //TODO: this is tuurible... have to go back to get inlcudes cuz I cant figure out how to cleanly get it above yet
            var potentialInjuries =
                DiffDiagUnitOfWork.InjuryRepo.GetAll(new[] { "InjuryWorkoutMatrixItems", "InjuryWorkoutMatrixItems.Workout", "InjurySignMatrixItems", 
                                                             "InjurySignMatrixItems.Sign", "InjuryCauseMatrixItems", "InjuryCauseMatrixItems.Cause" })
                                  .Where(p => distinctPotentialInjuries.Contains(p));
            
            Mapper.Map(potentialInjuries, potentialInjuryDtos);

            var givenSymptoms =
                DiffDiagUnitOfWork.SymptomResponseRepo.GetAll(new[] { "SymptomMatrixItem", "SymptomMatrixItem.InjurySymptomMatrixItems", "SymptomMatrixItem.Symptom", "SymptomMatrixItem.BodyPartMatrixItem", "SymptomMatrixItem.BodyPartMatrixItem.BodyPart" })
                                  .Where(p => p.DifferentialDiagnosisId == request.IdAsInt && p.GivenResponse > 0);

            //TODO: this whole service is a fucking mess... gotta be a better way
            foreach (var potentialInjuryDto in potentialInjuryDtos)
            {
                var givenSymptomDtos = new List<PotentialSymptomDto>();

                givenSymptoms.ForEach(
                    p =>
                    p.SymptomMatrixItem.InjurySymptomMatrixItems.Where(i => i.InjuryId == potentialInjuryDto.id)
                     .ForEach(i => givenSymptomDtos.Add(Mapper.Map<PotentialSymptomDto>(p))));

                potentialInjuryDto.givenSymptoms = givenSymptomDtos.ToArray();
            }



            if (potentialInjuryDtos.Count > 0)
                diagnosisReportDto.potentialInjuries = potentialInjuryDtos.ToArray();

            return Ok(new ApiResponse<DiagnosisReportDto>()
            {
                Resource = diagnosisReportDto
            });
        }

        #endregion

    }
}
