using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "ComponentSymptom", Namespace = "http://SportsWebPt.Platform")]
    public class ComponentSymptomDto
    {
        #region Properties

        [DataMember]
        public AreaComponentDto Component { get; set; }

        [DataMember]
        public SymptomDto[] Symptoms { get; set; }

        #endregion
    }
}
