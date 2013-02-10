using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "User", Namespace = "http://SportsWebPt.Platform")]
    public class UserDto
    {
        #region Properties

        [DataMember]
        public String EmailAddress { get; set; }

        [DataMember]
        public String FirstName { get; set; }

        [DataMember]
        public String LastName { get; set; }

        [DataMember]
        public String Phone { get; set; }

        [DataMember]
        public String SkypeHandle { get; set; }

        #endregion
    }
}
