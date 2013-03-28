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
            _userInfoUri = "https://graph.facebook.com/me?access_token=";
        }

        #endregion

        public override OAuthUser GetUserInfo()
        {
            var request =
                WebRequest.Create(String.Format("{0}{1}", _userInfoUri,
                                                Uri.EscapeDataString(_authorizationState.AccessToken)));

            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var graph = FacebookGraph.Deserialize(responseStream);
                    return new OAuthUser
                        {
                            EmailAddress = graph.EmailAddress,
                            FirstName = graph.FirstName,
                            LastName = graph.LastName,
                            ProviderId = graph.Id,
                            Locale = graph.Locale,
                            Gender = graph.Gender,
                            Provider = OAuthProvider.Facebook
                        };
                }
            }
        }

        protected override dynamic GetTokenInfo()
        {
            return null;
        }

    }
}
