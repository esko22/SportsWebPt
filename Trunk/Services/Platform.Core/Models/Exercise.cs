using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Exercise
    {
        #region Properties

        public int Id { get; set; }

        public String Name { get; set; }

        public String MedicalName { get; set; }

        public String Description { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public String StructuresInvolved { get; set; }

        public int Duration { get; set; }

        public int Sets { get; set; }

        public int Repititions { get; set; }

        public int PerWeek { get; set; }

        public int PerDay { get; set; }

        public int HoldFor { get; set; }

        public HoldType HoldType { get; set; }

        public ExerciseDifficulty Difficulty { get; set; }

        #endregion

        #region Navigation Propertes

        public ICollection<ExerciseCategoryMatrixItem> ExerciseCategoryMatrixItems { get; set; }

        public ICollection<ExerciseEquipmentMatrixItem> ExerciseEquipmentMatrixItems { get; set; }

        public ICollection<ExerciseVideoMatrixItem> ExerciseVideoMatrixItems { get; set; }

        public ICollection<ExerciseBodyRegionMatrixItem> ExerciseBodyRegionMatrixItems { get; set; }

        public ICollection<ExerciseBodyPartMatrixItem> ExerciseBodyPartMatrixItems { get; set; }

        public ICollection<PlanExerciseMatrixItem> PlanExerciseMatrixItemItems { get; set; }

        public ICollection<User> Users { get; set; }

        public ExercisePublishDetail PublishDetail { get; set; }

        #endregion
    }

    public class ExercisePublishDetail
    {
        #region Properties

        public int Id { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Exercise Exercise { get; set; }

        #endregion
        
    }
}
