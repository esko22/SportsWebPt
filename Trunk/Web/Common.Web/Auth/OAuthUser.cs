using System;

namespace SportsWebPt.Common.Web.Auth
{
    public class OAuthUser
    {
        #region Properties

        public String EmailAddress { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Gender { get; set; }

        public String Locale { get; set; }

        public String ProviderId { get; set; }

        public OAuthProvider Provider { get; set; }

        #endregion
    }
}
