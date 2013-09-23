using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class Video
    {
        #region Properties

        public int id { get; set; }

        public DateTime creationDate { get; set; }

        public String name { get; set; }

        public String medicalName { get; set; }

        public String description { get; set; }

        public String filename { get; set; }

        public String youtubeVideoId { get; set; }

        public string[] categories { get; set; }

        #endregion
    }
}
