using System;
using System.Security.Claims;
using BrockAllen.MembershipReboot;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Thinktecture.IdentityServer.Core;

namespace SportsWebPt.Identity.Services.Modules
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule()
        {

            Get["/register"] = _ =>
            {
                var model = new {title = "UYG Registration", signin = Request.Query["signin"]};
                return View["registration", model];
            };

            Post["/register"] = _ =>
            {

                var registrationData = this.Bind<LocalRegistrationModel>();
                var userAccountService = UserAccountServiceFactory.GetUserAccountService();
                if (userAccountService != null)
                {
                    var user = userAccountService.CreateAccount(registrationData.Username,
                        registrationData.Password,
                        registrationData.Email);

                    var claimCollection = new UserClaimCollection(new[]
                    {
                        new Claim("first name", registrationData.FirstName),
                        new Claim("last name", registrationData.LastName),
                        new Claim("name",
                            String.Format("{0} {1}", registrationData.FirstName, registrationData.LastName)),
                        new Claim("invite code", registrationData.InviteCode)
                    });

                    userAccountService.AddClaims(user.ID, claimCollection);
                }

                return new RedirectResponse("/core/" + Constants.RoutePaths.Login + "?signin=" + Request.Query["signin"]);
            };
        }

    }

    public class LocalRegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InviteCode { get; set; }
        public String Email { get; set; }
    }
}
