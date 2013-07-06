using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Video", Namespace = "http://SportsWebPt.Platform")]
    public class VideoDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public DateTime creationDate { get; set; }

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public String filename { get; set; }

        [DataMember]
        public String youtubeVideoId { get; set; }

        #endregion
    }
}
