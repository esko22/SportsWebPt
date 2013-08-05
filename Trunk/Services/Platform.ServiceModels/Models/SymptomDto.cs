using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Symptom", Namespace = "http://SportsWebPt.Platform")]
    public class SymptomDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public String renderType { get; set; }

        [DataMember]
        public String renderOptions { get; set; }

        [DataMember]
        public String renderTemplate { get; set; }

        #endregion
    }
}
