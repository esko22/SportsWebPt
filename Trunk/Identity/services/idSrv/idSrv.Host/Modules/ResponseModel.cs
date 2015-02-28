using System;
using SportsWebPt.Identity.Services.Core;

namespace SportsWebPt.Identity.Services
{
    public class ResponseModel
    {
        public String title { get; set; }

        public String signin { get; set; }

        public String error { get; set; }

        public String body1 { get; set; }

        public String body2 { get; set; }

        public String subject { get; set; }

        public String backText { get; set; }

        public String backUrl { get; set; }

        public String clientUrl { get; set; }

        public String resetkey { get; set; }
    }

    public class SportsWebPtResponseModel : ResponseModel
    {
        public SportsWebPtResponseModel()
        {
            backUrl = IdentityServerConfigSettings.Instance.DefaultClientUri;
            clientUrl = IdentityServerConfigSettings.Instance.DefaultClientUri;
            backText = "<i class=\"fa fa-long-arrow-left\"></i>&nbsp;Back to Sign In</a>";
            body2 = "If you have any questions, please send us a message at admin@sportswebpt.com";
        }
    }
}