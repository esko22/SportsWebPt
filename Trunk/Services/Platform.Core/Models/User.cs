using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class User
    {
        #region Properties

        public int Id { get; set; }

        public String EmailAddress { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String Phone { get; set; }

        public String SkypeHandle { get; set; }

        public String ProviderId { get; set; }

        public String Provider { get; set; }

        public String Locale { get; set; }

        public String Gender { get; set; }

        #endregion

    }
}
