using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class CauseDto
    {
        #region Properties

        public int Id { get; set; }

        public String Description { get; set; }

        public String Category { get; set; }

        public FilterDto Filter { get; set; }

        #endregion
    }
}
