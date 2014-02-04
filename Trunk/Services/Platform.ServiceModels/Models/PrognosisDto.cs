using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class PrognosisDto
    {
        #region Properties
        
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String Category { get; set; } 

        #endregion

    }

    public class InjuryPrognosisDto : PrognosisDto
    {
        #region Properties

        public int PrognosisId { get; set; }

        public String Duration { get; set; }

        #endregion
    }
}
