using System;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea("Main")]
    public class SecureController : Controller
    {

        #region Fields

        private readonly WebServerClient _client = AuthHelper.CreateClient();

        #endregion

        [GET("OAuth", IsAbsoluteUrl = true)]
        public ActionResult OAuth()
        {
            if (string.IsNullOrEmpty(Request.QueryString["code"]))
                return  InitAuth();

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
            state.Scope.Add("https://www.googleapis.com/auth/calendar");

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
            Session["auth"] = auth;

            var google = new GoogleProxy();
            var tv = new TokenValidator();

            var tokenInfo = google.GetTokenInfo(auth.AccessToken);
            tv.ValidateToken(tokenInfo, expectedAudience: "291493632670.apps.googleusercontent.com");

            var userInfo = google.GetUserInfo(auth.AccessToken);

            Session["user.name"] = userInfo.name;
            Session["user.birthday"] = userInfo.birthday;
            Session["user.locale"] = userInfo.locale;
            Session["user.userid"] = userInfo.id;
            Session["user.email"] = userInfo.email;

            // Later, if necessary:
            // bool success = client.RefreshAuthorization(auth);

            return new ViewResult();
        }
    }
}
