using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Operations
{
    public class DifferentialDiagnosisRequest : ApiResourceRequest<DifferentialDiagnosisDto>
    {
        #region Properties

        public SymptomResponseDto[] symptomResponses { get; set; }

        #endregion
    }
}
