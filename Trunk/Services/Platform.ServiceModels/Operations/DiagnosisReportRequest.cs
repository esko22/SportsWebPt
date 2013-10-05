using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/diagnosisReports/{id}", "GET")]
    public class DiagnosisReportRequest : AbstractResourceRequest, IReturn<ApiResponse<DiagnosisReportDto>>
    {}

    [Route("/differentialDiagnosis", "POST")]
    public class CreateDiagnosisReportRequest : DifferentialDiagnosisDto, IReturn<ApiResponse<DifferentialDiagnosisDto>>    
    {}

}
