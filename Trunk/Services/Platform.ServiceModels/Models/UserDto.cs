using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class UserDto
    {
        #region Properties

        public int Id { get; set; }

        public String EmailAddress { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String UserName { get; set; }

        public String Hash { get; set; }

        public String Phone { get; set; }

        public String SkypeHandle { get; set; }

        public String Locale { get; set; }

        public String Gender { get; set; }

        public String Provider { get; set; }

        public String ProviderId { get; set; }

        #endregion
    }
}
