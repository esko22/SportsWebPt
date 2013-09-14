using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Exercise", Namespace = "http://SportsWebPt.Platform")]
    public class ExerciseDto
    {
        #region Properties

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public EquipmentDto[] Equipment { get; set; }

        [DataMember]
        public VideoDto[] Videos { get; set; }

        [DataMember]
        public BodyRegionDto[] BodyRegions { get; set; }

        [DataMember]
        public BodyPartDto[] BodyParts { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String MedicalName { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public String PageName { get; set; }

        [DataMember]
        public String Tags { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        public String Difficulty { get; set; }

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

        [DataMember]
        public String HoldType { get; set; }

        [DataMember]
        public String[] Categories { get; set; }

        #endregion
    }

    [DataContract(Name = "PlanExercise", Namespace = "http://SportsWebPt.Platform")]
    public class PlanExerciseDto : ExerciseDto
    {
        [DataMember]
        public int ExerciseId { get; set; }
    }
}
