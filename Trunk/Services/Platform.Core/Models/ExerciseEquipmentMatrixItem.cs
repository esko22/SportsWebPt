using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class ExerciseEquipmentMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int ExcerciseId { get; set; }

        public int EquipmentId { get; set; }

        public virtual Exercise Exercise { get; set; }

        public virtual Equipment Equipment { get; set; }

        #endregion
    }
}
