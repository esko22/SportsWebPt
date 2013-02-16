using System.Net.Http;
using System.Net.Http.Headers;

using DotNetOpenAuth.OAuth2;
using Newtonsoft.Json.Linq;

namespace SportsWebPt.Platform.Web.Application
{
    public class GoogleProxy
    {
        public dynamic GetUserInfo(string authToken)
        {
            var userInfoUrl = "https://www.googleapis.com/oauth2/v1/userinfo";
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var response = hc.GetAsync(userInfoUrl).Result;
            dynamic userInfo = response.Content.ReadAsAsync<object>().Result;
            return userInfo;
        }

        public dynamic GetTokenInfo(string accessToken)
        {
            var verificationUri =
                    "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token="
                     + accessToken;

            var hc = new HttpClient();

            var response = hc.GetAsync(verificationUri).Result;
            dynamic tokenInfo = response.Content.ReadAsAsync<object>().Result;
            return tokenInfo;
        }

    }
}