using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Plan", Namespace = "http://SportsWebPt.Platform")]
    public class PlanDto
    {
        #region Properties

        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public PlanExerciseDto[] Exercises { get; set; }

        [DataMember]
        public String Category { get; set; }

        [DataMember]
        public String RoutineName { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public String StructuresInvolved { get; set; }

        [DataMember]
        public String Instructions { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        public String PageName { get; set; }

        [DataMember]
        public String Tags { get; set; }

        [DataMember]
        public BodyRegionDto[] BodyRegions { get; set; }

        #endregion
    }
}
