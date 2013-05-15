using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "DifferentialDiagnosisReport", Namespace = "http://SportsWebPt.Platform")]
    public class DifferentialDiagnosisReportDto : DifferentialDiagnosisDto
    {
        #region Properties

        [DataMember]
        public InjuryDto[] PotentialInjuries { get; set; }

        #endregion
    }
}
