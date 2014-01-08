using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class SignDto
    {
        #region Propeties

        public int Id { get; set; }

        public String Description { get; set; }

        public String Category { get; set; }

        public SignFilterDto Filter { get; set; }

        #endregion
    }
}
