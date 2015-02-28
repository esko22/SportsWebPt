using System;
using BrockAllen.MembershipReboot;
using Nancy;
using Nancy.Responses;
using SportsWebPt.Identity.Services;
using SportsWebPt.Identity.Services.Core;


namespace Illumina.Stargate.Identity.Host
{
    public class AccountVerificationModule : NancyModule
    {

        public AccountVerificationModule()
        {

            Get["/verifyEmail/{key}"] = _ =>
            {
                var userAccountService = UserAccountServiceFactory.GetUserAccountService();
                var account = userAccountService.GetByVerificationKey(_.key);

                if (account != null)
                {
                    userAccountService.SetConfirmedEmail(account.ID, account.Email);
                    var model = new SportsWebPtResponseModel
                    {
                        title = "Email Verification Success!",
                        body1 =
                            "You have successfully verified your account.",
                        body2 = "Please Go Back To Sign In Below",
                        subject = "Email Verification Success",
                    };

                    return View["informView", model];
                }

                return View["informView", InvalidLinkModel()];
            };

            Get["/cancelVerification/{key}"] = _ =>
            {
                var userAccountService = UserAccountServiceFactory.GetUserAccountService();
                if (userAccountService.GetByVerificationKey(_.key) == null)
                {
                    var model = InvalidLinkModel();
                    return View["informView", model];
                }

                    try
                    {
                        bool acClosed;
                        userAccountService.CancelVerification(_.key, out acClosed);

                        if (acClosed)
                        {
                            var model = new SportsWebPtResponseModel
                            {
                                title = "Account Creation Cancelled",
                                body1 =
                                    "You have successfully cancelled your account.",
                                subject = "Account Cancelled",
                            };

                            return View["informView", model];
                        }
                        else
                        {
                            var model = new SportsWebPtResponseModel
                            {
                                title = "Request Cancelled",
                                subject = "Request Cancelled"
                            };
                            return View["informView", model];
                        }
                    }
                    catch (Exception e)
                    {
                        var model = InvalidLinkModel();
                        return View["informView", model];
                    }
            };


        }

        public static Object AccountVerification(UserAccount account, string signin, string specificBody)
        {
            var userAccountService = UserAccountServiceFactory.GetUserAccountService();
            userAccountService.RequestAccountVerification(account.ID);
            var model = new SportsWebPtResponseModel
            {
                title = "SportsWebPT Email Confirmation",
                signin = signin,
                body1 = "We have sent an email to your inbox to verify your email. Please go to your email inbox and confirm your email address.",
                body2 = specificBody,
                subject = "Your Email Has Not Been Verified"
            };

            return model;
        }


        private Object InvalidLinkModel()
        {
            return new SportsWebPtResponseModel
            {
                title = "Invalid Link",
                body1 =
                    "This link you have used is no longer valid.",
                subject = "Invalid Link"
            };
        }

    }

}







