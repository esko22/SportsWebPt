using System;
using System.Linq;
using System.Security.Claims;
using BrockAllen.MembershipReboot;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Security;
using Thinktecture.IdentityServer.Core;

namespace SportsWebPt.Identity.Services.Modules
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule()
        {

            Get["/register"] = _ =>
            {
                var model = new { title = "SportsWebPt Registration", signin = Request.Query["signin"], errorMessage = ""};
                return View["registration", model];
            };

            Get["/register/login"] = _ =>
            {
                var model = new { title = "SportsWebPt Registration", signin = Request.Query["signin"], errorMessage = "" };
                return View["login", model];
            };


            Post["/register"] = _ =>
            {

                var registrationData = this.Bind<LocalRegistrationModel>();
                var userAccountService = UserAccountServiceFactory.GetUserAccountService();

                try
                {
                    if (userAccountService != null)
                    {
                        var user = userAccountService.CreateAccount(registrationData.Username,
                            registrationData.SignupPassword,
                            registrationData.Email);

                        var claimCollection = new UserClaimCollection(new[]
                        {
                            new Claim("first name", registrationData.FirstName),
                            new Claim("last name", registrationData.LastName),
                            new Claim("name",
                                String.Format("{0} {1}", registrationData.FirstName, registrationData.LastName))
                        });

                        userAccountService.AddClaims(user.ID, claimCollection);
                    }
                }
                catch (Exception ex)
                {
                    var model = new { title = "SportsWebPt Registration", signin = Request.Query["signin"], errorMessage = ex.Message };
                    return View["registration", model];
                }

                //return new RedirectResponse("/core/register/login?signin=" + Request.Query["signin"]);

                return new RedirectResponse("/core/" + Constants.RoutePaths.Login + "?signin=" + Request.Query["signin"]);
            };
        }

    }

    public class ExternalRegistrationModule : NancyModule
    {
        public ExternalRegistrationModule()
        {
            Get["/register/external"] = _ =>
            {
                var authentication = Context.GetAuthenticationManager().AuthenticateAsync(Constants.PartialSignInAuthenticationType).Result;
                if (authentication == null)
                    throw new Exception("Auth Failure");

                var model = new { title = "SportsWebPt External Registration", signin = Request.Query["signin"] };
                return View["externalRegistration", model];
            };

            Post["/register/external"] = _ =>
            {
                var authentication = Context.GetAuthenticationManager().AuthenticateAsync(Constants.PartialSignInAuthenticationType).Result;
                if (authentication == null)
                    throw new Exception("Auth Failure");

                var userAccountService = UserAccountServiceFactory.GetUserAccountService();

                if (userAccountService != null)
                {
                    var registrationData = this.Bind<ExternalRegistrationModel>();

                    var nameIdClaim = authentication.Identity.Claims.First(x => x.Type == Constants.ClaimTypes.ExternalProviderUserId);
                    var provider = nameIdClaim.Issuer;
                    var providerUserId = nameIdClaim.Value;
                    var acct = userAccountService.GetByLinkedAccount(provider, providerUserId);

                    var claimCollection = new UserClaimCollection(new[]
                    {
                        new Claim("username", registrationData.Username)
                    });

                    userAccountService.AddClaims(acct.ID, claimCollection);
                }

                return new RedirectResponse(authentication.Identity.Claims.Single(x => x.Type == Constants.ClaimTypes.PartialLoginReturnUrl).Value);

            };

        }
    }

    public class LocalRegistrationModel
    {
        public string Username { get; set; }
        public string SignupPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public String Email { get; set; }
    }

    public class ExternalRegistrationModel
    {
        public string Username { get; set; }
        public string State { get; set; }
    }
}
