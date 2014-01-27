using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class ExerciseDto
    {
        #region Properties

        public int Id { get; set; }

        public EquipmentDto[] Equipment { get; set; }

        public VideoDto[] Videos { get; set; }

        public BodyRegionDto[] BodyRegions { get; set; }

        public BodyPartDto[] BodyParts { get; set; }

        public String Name { get; set; }

        public String MedicalName { get; set; }

        public String Description { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public int Duration { get; set; }

        public String Difficulty { get; set; }

        public int Sets { get; set; }

        public int Repititions { get; set; }

        public int PerWeek { get; set; }

        public int PerDay { get; set; }

        public int HoldFor { get; set; }

        public String HoldType { get; set; }

        public String[] Categories { get; set; }

        #endregion
    }

    public class PlanExerciseDto : ExerciseDto
    {
        public int ExerciseId { get; set; }

        public int Order { get; set; }
    }
}
