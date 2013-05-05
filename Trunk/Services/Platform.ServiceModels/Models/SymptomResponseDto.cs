using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SymptomResponse", Namespace = "http://SportsWebPt.Platform")]
    public class SymptomResponseDto
    {
        #region Properties

        [DataMember]
        public int differentialDiagnosisId { get; set; }

        [DataMember]
        public int symptomMatrixItemId { get; set; }

        [DataMember]
        public String givenResponse { get; set; }

        #endregion
    }
}
