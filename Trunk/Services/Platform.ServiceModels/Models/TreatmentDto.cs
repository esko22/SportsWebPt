using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class TreatmentDto
    {
        #region Properties

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String Provider { get; set; }

        public String Category { get; set; }

        #endregion
    }
}
