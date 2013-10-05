
namespace SportsWebPt.Platform.ServiceModels
{
    public class DiagnosisReportDto : DifferentialDiagnosisDto
    {
        //TODO: is this a 'has a' or 'is a'?? Easier this way to leverage diff diag id
        //but we may need to persist the given results which will give a report id
        #region Properties

        public PotentialInjuryDto[] PotentialInjuries { get; set; }

        #endregion
    }
}
