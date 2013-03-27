using System;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Web.Auth;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea("Main")]
    public class AuthController : BaseController
    {

        #region Fields

        private readonly String _redirectCookieName = "SWPT-XSRF-RU";
        private Func<OAuthProvider, String, AuthWebServerClient> _authWebServerClientFactory;

        #endregion

        #region Construction

        public AuthController(Func<OAuthProvider,String,AuthWebServerClient> authWebServerClientFactory)
        {
            Check.Argument.IsNotNull(authWebServerClientFactory, "authWebServerClientFactory");
            _authWebServerClientFactory = authWebServerClientFactory;
        }

        #endregion


        [GET("Logon", IsAbsoluteUrl = true)]
        public ActionResult AuthForm()
        {
            return View(new AuthFormViewModel());
        }

        [GET("OAuth", IsAbsoluteUrl = true)]
        public ActionResult OAuth(string code, string returnUrl, string provider)
        {
            if (string.IsNullOrEmpty(code))
            {
                var authWebServerClient = GetOAuthWebServerClient(provider);
                var redirectTokens = String.Format("{0}|{1}", returnUrl, provider);
                var encryptedValue = Convert.ToBase64String(MachineKey.Protect(redirectTokens.ToUtf8ByteArray(), null));
                Response.Cookies.Add(new HttpCookie(_redirectCookieName)
                    {
                        Value = encryptedValue,
                        Expires = DateTime.Now.AddSeconds(300)
                    });

                return authWebServerClient.PrepareRequestUserAuthorization().AsActionResult();
            }

            return  OAuthCallback();
        }

        private AuthWebServerClient GetOAuthWebServerClient(string provider)
        {
            Check.Argument.IsNotNullOrEmpty(provider, "OAuthProvider");
            var oauthProvider = (OAuthProvider) Enum.Parse(typeof (OAuthProvider), provider, true);

            return _authWebServerClientFactory.Invoke(oauthProvider, Request.Url.GetLeftPart(UriPartial.Path));
        }

        [GET("Logout", IsAbsoluteUrl = true)]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("AuthForm");

        }

        private ActionResult OAuthCallback()
        {
            var redirectTokens = GetRedirectTokens();
            var redirectUri = redirectTokens[0];
            var authWebServerClient = GetOAuthWebServerClient(redirectTokens[1]);
            authWebServerClient.ProcessUserAuthorization(Request);
            
            //authWebServerClient.ValidateToken("136219353860.apps.googleusercontent.com");

            var userInfo = authWebServerClient.GetUserInfo();
            
            if (userInfo != null)
            {
                var userId =
                    _userManagementService.AddUser(new User() { emailAddress = userInfo.EmailAddress, firstName = userInfo.FirstName, lastName = userInfo.LastName });

                //TODO: consider changing this to email addy
                FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);
            }

            // Later, if necessary:
            // bool success = client.RefreshAuthorization(auth);

            if (Url.IsLocalUrl(redirectUri))
                return Redirect(redirectUri); 

            return Redirect("/"); 
        }

        private String[] GetRedirectTokens()
        {
            var cookie = Request.Cookies[_redirectCookieName];

            if (cookie != null 
                && !String.IsNullOrEmpty(cookie.Value))
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Set(cookie);
 
                return Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(cookie.Value), null)).Split('|');
            }

            return new string[]{};
        }

        protected override void Dispose(bool disposing)
        {
            if(_userManagementService != null)
                _userManagementService.Dispose();
        }
    }
}
