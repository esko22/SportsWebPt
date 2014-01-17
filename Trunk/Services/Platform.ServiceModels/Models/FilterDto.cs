using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class FilterDto
    {
        #region Propeties

        public int Id { get; set; }

        public String FilterCategory { get; set; }

        public FilterTypeDto FilterType { get; set; }

        #endregion

    }

    public enum FilterTypeDto
    {
        Sign,
        Cause
    } 
}
