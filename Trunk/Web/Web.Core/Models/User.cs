using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Web.Core
{
    public class User
    {
        //TODO: look into case sensitivity issues. Props have to match case so work
        //with backbone model mappings, atleast the id prop

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

        public IEnumerable<UserFavorite> favorites { get; set; } 

        #endregion

    }
}
