using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Exercise
    {
        #region Properties

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public int Duration { get; set; }

        public ExerciseDifficulty Difficulty { get; set; }

        public ICollection<ExerciseEquipmentMatrixItem> ExerciseEquipmentMatrixItems { get; set; }

        public ICollection<ExerciseVideoMatrixItem> ExerciseVideoMatrixItems { get; set; } 

        #endregion
    }
}
