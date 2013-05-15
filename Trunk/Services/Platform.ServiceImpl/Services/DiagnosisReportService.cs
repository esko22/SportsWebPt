using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceImpl.Operations;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class DiagnosisReportService : LoggingRestServiceBase<DiagnosisReportRequest, ApiResponse<DifferentialDiagnosisReportDto>>
    {
        #region Properties

        public IDiffDiagUnitOfWork DiffDiagUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(DiagnosisReportRequest request)
        {
            var differentialDiagEntity = DiffDiagUnitOfWork.DiffDiagRepo.GetById(request.IdAsInt);
            var diagnosisReportDto = Mapper.Map<DifferentialDiagnosisReportDto>(differentialDiagEntity);




            var potentialInjuryDtos = new List<InjuryDto>();
            //Mapper.Map(potentialInjuries, potentialInjuryDtos);

            diagnosisReportDto.PotentialInjuries = potentialInjuryDtos.ToArray();

            return Ok(new ApiResponse<DifferentialDiagnosisDto>()
            {
                Resource = diagnosisReportDto
            });
        }

        #endregion

    }
}
