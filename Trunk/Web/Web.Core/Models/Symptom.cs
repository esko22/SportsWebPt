using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class Symptom
    {
        #region Properties

        public int id { get; set; }

        public String name { get; set; }

        public String description { get; set; }

        public String renderType { get; set; }

        public String renderTemplate { get; set; }

        public String renderOptions { get; set; }

        #endregion
    }
}
