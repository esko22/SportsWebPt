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
            var differentialDiagEntity = Mapper.Map<DifferentialDiagnosis>(request.Resource);
            DiffDiagUnitOfWork.DiffDiagRepo.Add(differentialDiagEntity);
            DiffDiagUnitOfWork.Commit();

            return Ok(new ApiResponse<DifferentialDiagnosisDto>()
            {
                Resource = Mapper.Map<DifferentialDiagnosisDto>(differentialDiagEntity)
            });
        }

        #endregion

    }
}
