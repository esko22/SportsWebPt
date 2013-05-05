using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceImpl.Operations;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class DifferentialDiagnosisService : LoggingRestServiceBase<DifferentialDiagnosisRequest, ApiResponse<DifferentialDiagnosisDto>>
    {
        #region Properties

        public IDiffDiagUnitOfWork DiffDiagUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(DifferentialDiagnosisRequest request)
        {
            var differentialDiagnosisDto = request.Resource;
            var differentialDiagEntity = Mapper.Map<DifferentialDiagnosis>(differentialDiagnosisDto);

            DiffDiagUnitOfWork.DiffDiagRepo.Add(differentialDiagEntity);
            DiffDiagUnitOfWork.Commit();

            if (request.symptomResponses != null)
            {
                foreach (var symptomResponseDto in request.symptomResponses)
                {
                    var symptomResponse = Mapper.Map<SymptomResponse>(symptomResponseDto);
                    symptomResponse.DifferentialDiagnosisId = differentialDiagEntity.Id;

                    DiffDiagUnitOfWork.SymptomResponseRepo.Add(symptomResponse);
                }

                DiffDiagUnitOfWork.Commit();
            }

            return Ok(new ApiResponse<DifferentialDiagnosisDto>()
            {
                Resource = Mapper.Map<DifferentialDiagnosisDto>(differentialDiagEntity)
            });
        }

        #endregion

    }
}
