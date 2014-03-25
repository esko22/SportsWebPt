using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsWebPt.Platform.Web.Application
{
    public class LoginCommand
    {
        #region Properties

        public String userEmail { get; set; }

        public String username { get; set; }

        public String userPassword { get; set; }

        public String signupPassword { get; set; }

        public String signupEmail { get; set; }

        public String returnUrl { get; set; }

        #endregion
    }
}