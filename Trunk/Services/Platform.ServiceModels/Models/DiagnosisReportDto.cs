using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "DiagnosisReport", Namespace = "http://SportsWebPt.Platform")]
    public class DiagnosisReportDto : DifferentialDiagnosisDto
    {
        //TODO: is this a 'has a' or 'is a'?? Easier this way to leverage diff diag id
        //but we may need to persist the given results which will give a report id
        #region Properties

        [DataMember]
        public InjuryDto[] potentialInjuries { get; set; }

        #endregion
    }
}
