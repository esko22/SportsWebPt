using System;

using BrockAllen.MembershipReboot;
using Illumina.Stargate.Identity.Host;
using Nancy;
using Nancy.ModelBinding;

using SportsWebPt.Identity.Services.Core;

namespace SportsWebPt.Identity.Services.Modules
{
    public class UsernameRecoveryModule : NancyModule
    {

        public UsernameRecoveryModule()
        {
            Get["/usernameRecovery"] = _ =>
            {
                var model = new SportsWebPtResponseModel
                {
                    title = "SportsWebPT Username Reminder", signin = Request.Query["signin"]
                };
                return View["usernameRecovery", model];
            };

            Post["/usernameRecovery"] = _ =>
            {
                var userAccountService = UserAccountServiceFactory.GetUserAccountService();
                var userNameRecoveryData = this.Bind<CredentialRecoveryModel>();

                var account = userAccountService.GetByEmail(userNameRecoveryData.Email);

                if (account != null && account.IsAccountVerified)
                {
                    userAccountService.SendUsernameReminder(userNameRecoveryData.Email);

                    var model = new SportsWebPtResponseModel
                    {
                        title = "SportsWebPT Username Sent",
                        signin = Request.Query["signin"],
                        body1 = "We have sent information regarding your username to your email.",
                        subject = "Your Username is On Its Way"
                    };
                    return View["informView", model];
                }

                if (account != null)
                {
                    var model = AccountVerificationModule.AccountVerification(account, Request.Query["signin"], "Once your email has been verified, you can recover your username from the link on the Sign In page.");
                    return View["informView", model];

                }
                else
                {
                    var model = new SportsWebPtResponseModel
                    {
                        title = "SportsWebPT Username Reminder", 
                        signin = Request.Query["signin"], 
                        error = "Email doesn't exist"
                    };
                    return View["usernameRecovery", model];
                }
            };
        }

    }

    public class CredentialRecoveryModel
    {
        public String Email { get; set; }
    }

}