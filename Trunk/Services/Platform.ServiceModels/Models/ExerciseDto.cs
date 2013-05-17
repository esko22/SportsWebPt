using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Exercise", Namespace = "http://SportsWebPt.Platform")]
    public class ExerciseDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public EquipmentDto[] equipment { get; set; }

        [DataMember]
        public VideoDto[] videos { get; set; }

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public int duration { get; set; }

        [DataMember]
        public String difficulty { get; set; }

        #endregion
    }
}
