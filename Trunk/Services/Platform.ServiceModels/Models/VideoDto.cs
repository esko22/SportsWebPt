using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Video", Namespace = "http://SportsWebPt.Platform")]
    public class VideoDto
    {
        #region Properties

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String MedicalName { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public String Filename { get; set; }

        [DataMember]
        public String YoutubeVideoId { get; set; }

        [DataMember]
        public String[] Categories { get; set; }

        #endregion
    }
}
