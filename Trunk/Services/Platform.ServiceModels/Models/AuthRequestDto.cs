using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "AuthRequest", Namespace = "http://SportsWebPt.Platform")]
    public class AuthRequestDto
    {
        #region Properties

        [DataMember]
        public String emailAddress { get; set; }

        [DataMember]
        public String hash { get; set; }

        #endregion
    }
}
