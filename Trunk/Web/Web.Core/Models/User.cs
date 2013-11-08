using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class User
    {
        #region Properties
        
        public String emailAddress { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public String phone { get; set; }

        public String hash { get; set; }

        public String skypeHandle { get; set; }

        public String userName { get; set; }

        public int id { get; set; }

        public String providerId { get; set; }

        public String provider { get; set; }

        public String locale { get; set; }

        public String gender { get; set; }

        public IEnumerable<Favorite> videos { get; set; }

        public IEnumerable<Favorite> plans { get; set; }

        public IEnumerable<Favorite> injuries { get; set; }
        
        public IEnumerable<Favorite> exercises { get; set; } 

        #endregion

    }
}
