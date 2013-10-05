using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class VideoDto
    {
        #region Properties

        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public String Name { get; set; }

        public String MedicalName { get; set; }

        public String Description { get; set; }

        public String Filename { get; set; }

        public String YoutubeVideoId { get; set; }

        public String[] Categories { get; set; }

        #endregion
    }
}
