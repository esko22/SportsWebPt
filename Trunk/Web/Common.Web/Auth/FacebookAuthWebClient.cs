using System;
using System.Net;
using System.Web;

using DotNetOpenAuth.ApplicationBlock.Facebook;
using DotNetOpenAuth.OAuth2;

namespace SportsWebPt.Common.Web.Auth
{
    public class FacebookAuthWebClient : AuthWebServerClient
    {
        #region Fields

        private static readonly AuthorizationServerDescription _authServerDescription = new AuthorizationServerDescription
            {
                TokenEndpoint = new Uri("https://graph.facebook.com/oauth/access_token"),
                AuthorizationEndpoint = new Uri("https://graph.facebook.com/oauth/authorize")
            };

        #endregion

        #region Construction

        public FacebookAuthWebClient(String appIdentifier, String appSecret)
            : base(_authServerDescription, appIdentifier, appSecret)
        {
            _authorizationState = new AuthorizationState();
            _authorizationState.Scope.Add("email");
        }

        #endregion

        public override OAuthUser GetUserInfo()
        {
            var request = WebRequest.Create("https://graph.facebook.com/me?access_token=" + Uri.EscapeDataString(_authorizationState.AccessToken));
            var user = new OAuthUser();

            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var graph = FacebookGraph.Deserialize(responseStream);
                    user.EmailAddress = HttpUtility.HtmlEncode(graph.EmailAddress);
                    user.FirstName = HttpUtility.HtmlEncode(graph.FirstName);
                    user.LastName = HttpUtility.HtmlEncode(graph.LastName);
                }
            }

            return user;
        }

        protected override dynamic GetTokenInfo()
        {
            return null;
        }

    }
}
