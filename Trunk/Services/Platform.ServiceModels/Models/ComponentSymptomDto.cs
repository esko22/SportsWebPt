using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SymptomaticComponent", Namespace = "http://SportsWebPt.Platform")]
    public class SymptomaticComponentDto : AreaComponentDto
    {
        #region Properties

        [DataMember]
        public SymptomDto[] Symptoms { get; set; }

        #endregion
    }
}
