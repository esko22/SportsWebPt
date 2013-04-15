using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using DotNetOpenAuth.OAuth2;

using Newtonsoft.Json;

namespace SportsWebPt.Common.Web.Auth
{
    public class GoogleAuthWebClient : AuthWebServerClient
    {
        #region Fields

        private static readonly AuthorizationServerDescription _authServerDescription = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(@"https://accounts.google.com/o/oauth2/auth"),
                TokenEndpoint = new Uri(@"https://accounts.google.com/o/oauth2/token"),
                ProtocolVersion = ProtocolVersion.V20
            };

        #endregion

        #region Construction

        public GoogleAuthWebClient(String clientIdentifier, String clientSecret, String callbackUri)
            : base(_authServerDescription, clientIdentifier, clientSecret)
        {
            _authorizationState = new AuthorizationState {Callback = new Uri(callbackUri)};
            _authorizationState.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
            _authorizationState.Scope.Add("https://www.googleapis.com/auth/userinfo.email");

            _expectedAudience = clientIdentifier;
            _userInfoUri = "https://www.googleapis.com/oauth2/v1/userinfo";

            //state.Scope.Add("https://www.googleapis.com/auth/calendar");
        }

        #endregion

        public override OAuthUser GetUserInfo()
        {
            var userInfo = JsonConvert.DeserializeObject<Dictionary<String,String>>(DoGetUserInfo().ToString());

            return new OAuthUser()
                {
                    EmailAddress = userInfo["email"],
                    FirstName = userInfo["given_name"],
                    LastName = userInfo["family_name"],
                    ProviderId = userInfo["id"],
                    Locale = userInfo["locale"],
                    Provider = OAuthProvider.Google
                };
        }

        private dynamic DoGetUserInfo()
        {
            ValidateToken();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authorizationState.AccessToken);
            var response = httpClient.GetAsync(_userInfoUri).Result;

            return response.Content.ReadAsAsync<object>().Result;
        }

        protected override dynamic GetTokenInfo()
        {
            var verificationUri =
                    "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token="
                     + _authorizationState.AccessToken;

            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(verificationUri).Result;

            dynamic tokenInfo = response.Content.ReadAsAsync<object>().Result;
            return tokenInfo;
        }
    }
}
