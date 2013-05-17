using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Cause", Namespace = "http://SportsWebPt.Platform")]
    public class CauseDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String description { get; set; }

        #endregion
    }
}
