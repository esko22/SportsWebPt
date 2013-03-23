using System;
using System.Web;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Web.Attributes;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea("Main")]
    public class AuthController : BaseController
    {

        #region Fields

        private readonly WebServerClient _client = AuthHelper.CreateClient();
        private readonly String _returnUrlCookieName = "SWPT-XSRF-RU";
        private String _redirectUri = "/";

        #endregion

        [GET("Logon", IsAbsoluteUrl = true)]
        public ActionResult AuthForm()
        {
            return View(new AuthFormViewModel());
        }

        [GET("OAuth", IsAbsoluteUrl = true)]
        public ActionResult OAuth(string code, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    var encryptedValue = Convert.ToBase64String(MachineKey.Protect(returnUrl.ToUtf8ByteArray(), null));
                    Response.Cookies.Add(new HttpCookie(_returnUrlCookieName)
                        {
                            Value = encryptedValue,
                            Expires = DateTime.Now.AddSeconds(300)
                        });
                }

                return InitAuth();
            }

            return  OAuthCallback();
        }

        private ActionResult InitAuth()
        {
            var state = new AuthorizationState();
            var uri = Request.Url.AbsoluteUri;
            uri = RemoveQueryStringFromUri(uri);
            state.Callback = new Uri(uri);

            state.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
            state.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
            //state.Scope.Add("https://www.googleapis.com/auth/calendar");
            var r = _client.PrepareRequestUserAuthorization(state);

            return r.AsActionResult();
        }

        private static string RemoveQueryStringFromUri(string uri)
        {
            var index = uri.IndexOf('?');
          
            if (index > -1)
                uri = uri.Substring(0, index);
          
            return uri;
        }

        private ActionResult OAuthCallback()
        {
            var auth = _client.ProcessUserAuthorization(this.Request);
            //Session["auth"] = auth;

            var google = new GoogleProxy();
            var tv = new TokenValidator();

            var tokenInfo = google.GetTokenInfo(auth.AccessToken);
            tv.ValidateToken(tokenInfo, expectedAudience: "136219353860.apps.googleusercontent.com");

            var userInfo = (new JavaScriptSerializer()).Deserialize<dynamic>(google.GetUserInfo(auth.AccessToken).ToString());
            var userId = 0;

            if (userInfo != null)
            {
                userId =
                    _userManagementService.AddUser(new User() {emailAddress = userInfo["email"], firstName = userInfo["given_name"], lastName = userInfo["family_name"]});
                //TODO: consider changing this to email addy
                FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);
            }

            // Later, if necessary:
            // bool success = client.RefreshAuthorization(auth);

            
            var cookie = Request.Cookies[_returnUrlCookieName];

            if (cookie != null && !String.IsNullOrEmpty(cookie.Value))
            {
                var redirectUri = Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(cookie.Value), null));

                if (Url.IsLocalUrl(redirectUri))
                {
                    _redirectUri = redirectUri;
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Set(cookie);
                }
            }

            return Redirect(_redirectUri); 
        }

        protected override void Dispose(bool disposing)
        {
            if(_userManagementService != null)
                _userManagementService.Dispose();
        }
    }
}
