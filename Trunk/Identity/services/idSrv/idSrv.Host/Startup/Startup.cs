using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Nancy.Owin;
using Owin;

using SportsWebPt.Identity.Services.Core;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Logging;
using Thinktecture.IdentityServer.Host.Config;

[assembly: OwinStartup("LocalTest", typeof(Thinktecture.IdentityServer.Host.Startup))]

namespace Thinktecture.IdentityServer.Host
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            // uncomment to enable HSTS headers for the host
            // see: https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security
            //app.UseHsts();

            var factory = Factory.Configure();

            app.Map("/core", coreApp =>
            {

                LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());
                IdentityServerOptions = new IdentityServerOptions
                {
                    IssuerUri = "https://idsrv3.com",
                    SiteName = "SportsWebPt",
                    SigningCertificate = Cert.Load(),
                    RequireSsl = false,
                    PublicHostName = IdentityServerConfigSettings.Instance.PublicHostName,
                    Factory = factory,
                    CorsPolicy = CorsPolicy.AllowAll,
                    AuthenticationOptions = new AuthenticationOptions()
                    {
                        IdentityProviders = ConfigureIdentityProviders,
                        LoginPageLinks = new[]
                        {
                            new LoginPageLink
                            {
                                Text = "Register",
                                Href = "/core/register"
                            },
                            new LoginPageLink
                            {
                                Text = "Forgot Username",
                                Href = "/core/usernameRecovery"
                            },
                            new LoginPageLink
                            {
                                Text = "Forgot Password",
                                Href = "/core/passwordRecovery"
                            }
                        }
                    }
                };
                coreApp.UseIdentityServer(IdentityServerOptions);
                coreApp.UseNancy(new NancyOptions() { });
            });
        }

        internal static IdentityServerOptions IdentityServerOptions { get; private set; }

        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var google = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = ConfigurationManager.AppSettings["googleClientKey"],
               ClientSecret = ConfigurationManager.AppSettings["googleClientSecret"],
               AuthenticationType = "Google",
               SignInAsAuthenticationType = signInAsType
            };
            app.UseGoogleAuthentication(google);

            var fb = new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                SignInAsAuthenticationType = signInAsType,
                AppId = ConfigurationManager.AppSettings["facebookClientKey"],
                AppSecret = ConfigurationManager.AppSettings["facebookClientSecret"],
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        if(String.IsNullOrEmpty(context.Email))
                            throw new ArgumentException("Email Permission Must Be Granted");

                        return Task.FromResult(0);
                    }
                }
            };

            fb.Scope.Add("public_profile");
            fb.Scope.Add("email");

            app.UseFacebookAuthentication(fb);
        }
    }
}