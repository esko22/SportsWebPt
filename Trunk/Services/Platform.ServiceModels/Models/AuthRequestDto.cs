using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class AuthRequestDto
    {
        #region Properties

        public String EmailAddress { get; set; }

        public String Hash { get; set; }

        #endregion
    }
}
