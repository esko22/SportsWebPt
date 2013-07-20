using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Plan", Namespace = "http://SportsWebPt.Platform")]
    public class PlanDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }
        
        [DataMember]
        public ExerciseDto[] exercises { get; set; }

        [DataMember]
        public String category { get; set; }

        [DataMember]
        public String routineName { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public String musclesInvolved { get; set; }

        [DataMember]
        public int duration { get; set; }

        [DataMember]
        public String PageName { get; set; }

        [DataMember]
        public String Tags { get; set; }

        [DataMember]
        public BodyRegionDto[] bodyRegions { get; set; }

        #endregion
    }
}
