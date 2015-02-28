using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Illumina.Stargate.Identity.Host;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using SportsWebPt.Identity.Services.Core;

namespace SportsWebPt.Identity.Services.Modules
{
    public class PasswordResetModule : NancyModule
    {

        public PasswordResetModule()
        {
            Get["/passwordRecovery"] = _ =>
            {
                var model =
                    new SportsWebPtResponseModel
                    {
                        title = "SportsWebPT Password Reset",
                        signin = Request.Query["signin"],
                    };
                return View["passwordrecovery", model];
            };

            Post["/passwordRecovery"] = _ =>
            {
                var userAccountService = UserAccountServiceFactory.GetUserAccountService();
                var resetPasswordData = this.Bind<CredentialRecoveryModel>();
                var account = userAccountService.GetByEmail(resetPasswordData.Email);

                if (account != null && account.IsAccountVerified)
                {
                    userAccountService.ResetPassword(account.Email);
                    var model = new SportsWebPtResponseModel
                    {
                        title = "SportsWebPT Password Reset",
                        signin = Request.Query["signin"],
                        body1 = "We have sent instructions to your email with a link to reset your password.",
                        subject = "Password Instructions Are on Their Way"
                    };
                    return View["informView", model];
                }
                else if (account != null)
                {
                    var model = AccountVerificationModule.AccountVerification(account, Request.Query["signin"],
                        "Once your email has been verified, you can reset your password from the link on the Sign In page.");
                    return View["informView", model];

                }
                else
                {
                    var model =
                        new SportsWebPtResponseModel
                        {
                            title = "SportsWebPT Password Reset",
                            signin = Request.Query["signin"],
                            error = "Email doesn't exist"
                        };
                    return View["passwordrecovery", model];
                }
            };

            Get["/passwordReset/{key}"] = _ =>
            {
                var userAccountService = UserAccountServiceFactory.GetUserAccountService();
                if (userAccountService.GetByVerificationKey(_.key) == null)
                {
                    var model = new SportsWebPtResponseModel
                    {
                        title = "Invalid Reset Link",
                        body1 =
                            "This link you have used to reset your password is no longer valid. Please go back to the Sign In page to reset your password.",
                        subject = "Invalid Password Reset Link"
                    };

                    return View["informView", model];
                }
                else
                {

                    var model =
                        new SportsWebPtResponseModel
                        {
                            title = "SportsWebPT Password Change",
                            signin = Request.Query["signin"],
                            resetkey = _.key
                        };
                    return View["passwordReset", model];
                }
            };

            Post["/passwordReset"] = _ =>
            {
                var passwordResetData = this.Bind<PasswordResetModel>();

                if (passwordResetData.ConfirmPassword != passwordResetData.Password)
                {
                    var model =
                        new SportsWebPtResponseModel
                        {
                            title = "SportsWebPT Password Change",
                            error = "Password does not match the confirm password.",
                            resetkey = passwordResetData.ResetKey
                        };
               
                    return View["passwordReset", model];
                }

                var userAccountService = UserAccountServiceFactory.GetUserAccountService();
                    try
                    {
                        bool status = userAccountService.ChangePasswordFromResetKey(passwordResetData.ResetKey,
                            passwordResetData.Password);
                        if (status)
                        {
                            var successModel = new SportsWebPtResponseModel
                            {
                                title = "Password Reset Success!",
                                body1 =
                                    "You have successfully reset your password.",
                                body2 = "Please Go Back To Sign In Below",
                                subject = "Password Reset Success",
                            };

                            return View["informView", successModel];

                        }

                        var model = new SportsWebPtResponseModel
                            {
                                title = "Invalid Reset Link",
                                body1 =
                                    "This link you have used to reset your password is no longer valid. Please go back to the Sign In page to reset your password.",
                                subject = "Invalid Reset Link"
                            };

                            return View["informView", model];
                        
                    }
                    catch (ValidationException e)
                    {
                        var error = e.Message;
                        var model =
                            new SportsWebPtResponseModel
                            {
                                title = "SportsWebPT Password Change",
                                error = error,
                                resetkey = passwordResetData.ResetKey
                            };
                        return View["passwordReset", model];
                    }
                
            };
        }
    }

    public class PasswordResetModel
    {
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
        public String ResetKey { get; set; }
    }
}