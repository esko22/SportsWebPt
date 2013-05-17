using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Video
    {
        #region Properties

        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String Filename { get; set; }

        public ICollection<ExerciseVideoMatrixItem> ExerciseVideoMatrixItems { get; set; }  

        #endregion
    }
}
