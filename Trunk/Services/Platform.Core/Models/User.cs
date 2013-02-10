using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Core.Models
{
    public class User
    {
        #region Properties

        public int Id { get; set; }

        public String EmailAddress { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Phone { get; set; }

        public String SkypeHandle { get; set; }

        #endregion

    }
}
