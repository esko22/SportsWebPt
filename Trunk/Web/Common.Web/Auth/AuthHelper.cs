﻿using System;

using DotNetOpenAuth.OAuth2;

namespace SportsWebPt.Common.Web.Auth
{
    public class AuthHelper
    {
        public static WebServerClient CreateClient()
        {
            var desc = GetAuthServerDescription();
            var client = new WebServerClient(desc, clientIdentifier: "136219353860.apps.googleusercontent.com");
            client.ClientCredentialApplicator = ClientCredentialApplicator.PostParameter("SNzL1wJ1Pf_EdiwYrXh0kvtN");
            return client;
        }

        public static AuthorizationServerDescription GetAuthServerDescription()
        {
            var authServerDescription = new AuthorizationServerDescription();
            authServerDescription.AuthorizationEndpoint = new Uri(@"https://accounts.google.com/o/oauth2/auth");
            authServerDescription.TokenEndpoint = new Uri(@"https://accounts.google.com/o/oauth2/token");
            authServerDescription.ProtocolVersion = ProtocolVersion.V20;
            return authServerDescription;
        }
    }

}