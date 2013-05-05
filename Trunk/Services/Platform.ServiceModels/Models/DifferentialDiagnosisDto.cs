using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "DifferentialDiagnosis", Namespace = "http://SportsWebPt.Platform")]
    public class DifferentialDiagnosisDto
    {
        #region Properties

        [DataMember]
        public int differentialDiagnosisId { get; set; }

        [DataMember]
        public DateTime submittedOn { get; set; }

        [DataMember]
        public DateTime reviewedOn { get; set; }

        [DataMember]
        public int submittedBy { get; set; }

        [DataMember]
        public int submittedFor { get; set; }

        #endregion

    }
}
