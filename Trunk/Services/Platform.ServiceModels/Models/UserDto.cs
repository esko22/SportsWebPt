using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceContracts.Models
{
    [DataContract(Name = "User", Namespace = "http://SportsWebPt.Platform")]
    public class UserDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String emailAddress { get; set; }

        [DataMember]
        public String firstName { get; set; }

        [DataMember]
        public String lastName { get; set; }

        [DataMember]
        public String userName { get; set; }

        [DataMember]
        public String password { get; set; }

        [DataMember]
        public String phone { get; set; }

        [DataMember]
        public String skypeHandle { get; set; }

        #endregion
    }
}
