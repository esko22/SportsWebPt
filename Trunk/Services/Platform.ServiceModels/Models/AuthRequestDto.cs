using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class AuthRequestDto
    {
        #region Properties

        public String emailAddress { get; set; }

        public String hash { get; set; }

        #endregion
    }
}
