using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class SymptomDto
    {
        #region Properties

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String RenderType { get; set; }

        public String RenderOptions { get; set; }

        public String RenderTemplate { get; set; }

        #endregion
    }
}
