using System;
using System.Web;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;

namespace SportsWebPt.Common.Web.Auth
{
    public abstract class AuthWebServerClient : WebServerClient
    {
        #region Fields

        protected IAuthorizationState _authorizationState;
        protected String _expectedAudience = String.Empty;
        protected String _userInfoUri = String.Empty;

        #endregion

        #region Construction

        protected AuthWebServerClient(AuthorizationServerDescription authorizationServerDescription,
                                      String clientIdentifier,String clientSecret)
            : base(authorizationServerDescription, clientIdentifier)
        {
            ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(clientSecret);
        }

        #endregion

        #region Methods

        public abstract OAuthUser GetUserInfo();

        protected abstract dynamic GetTokenInfo();

        public OutgoingWebResponse PrepareRequestUserAuthorization()
        {
            return PrepareRequestUserAuthorization(_authorizationState);
        }

        public new IAuthorizationState ProcessUserAuthorization(HttpRequestBase request = null)
        {
            _authorizationState = base.ProcessUserAuthorization(request);
            return _authorizationState;
        }

        public void ValidateToken()
        {
            var tokenInfo = GetTokenInfo();
            var audience = tokenInfo.audience.ToString();

            if (string.IsNullOrEmpty(audience) || audience != _expectedAudience)
                throw new HttpException("tokes with unexpected audience: ");
        }

        #endregion
    }
}
