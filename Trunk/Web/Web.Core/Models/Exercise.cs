using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class Exercise
    {
        #region Properties

        public int id { get; set; }

        public IEnumerable<Equipment> equipment { get; set; }

        public IEnumerable<Video> videos { get; set; }

        public String name { get; set; }

        public String description { get; set; }

        public String tags { get; set; }

        public String pageName { get; set; }

        public int duration { get; set; }

        public String difficulty { get; set; }

        #endregion
    }
}
