using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using ServiceStack.ServiceInterface.ServiceModel;
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

            var potentialInjuries =
                DiffDiagUnitOfWork.SymptomResponseRepo.GetAll()
                                  .Where(p => p.DifferentialDiagnosisId == request.IdAsInt && p.GivenResponse > 0)
                                  .Join(DiffDiagUnitOfWork.InjurySymptomMatrixItemRepo.GetAll(),
                                        sd => sd.SymptomMatrixItemId, ismi => ismi.SymptomMatrixItemId,
                                        (detail, item) => new {detail, item}).Where(isimisd => isimisd.detail.GivenResponse > isimisd.item.ThresholdValue)
                                  .Join(DiffDiagUnitOfWork.InjuryRepo.GetAll(), ismisd => ismisd.item.InjuryId,
                                        injury => injury.Id, (ismisd, injury) => injury).Distinct();

            var potentialInjuryDtos = new List<InjuryDto>();
            Mapper.Map(potentialInjuries, potentialInjuryDtos);

            //foreach (var potentialInjuryDto in potentialInjuryDtos)
            //{
            //    potentialInjuryDto.workoutDtos = 
            //}
            

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
