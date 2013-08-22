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
        public BodyRegionDto[] bodyRegions { get; set; }

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public String MedicalName { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public String pageName { get; set; }

        [DataMember]
        public String tags { get; set; }

        [DataMember]
        public int duration { get; set; }

        [DataMember]
        public String difficulty { get; set; }

        [DataMember]
        public int Sets { get; set; }

        [DataMember]
        public int Repititions { get; set; }

        [DataMember]
        public int PerWeek { get; set; }

        [DataMember]
        public int PerDay { get; set; }

        [DataMember]
        public int HoldFor { get; set; }

        #endregion
    }

    [DataContract(Name = "PlanExercise", Namespace = "http://SportsWebPt.Platform")]
    public class PlanExerciseDto : ExerciseDto
    {
        [DataMember]
        public int ExerciseId { get; set; }
    }
}
