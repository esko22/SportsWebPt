using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Sign", Namespace = "http://SportsWebPt.Platform")]
    public class SignDto
    {
        #region Propeties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public String category { get; set; }

        #endregion
    }
}
