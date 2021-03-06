﻿using System;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

using DotNetOpenAuth.Messaging;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Utilities.Security;
using SportsWebPt.Common.Web.Auth;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea]
    public class AuthController : BaseController
    {

        #region Fields

        private readonly String _redirectCookieName = "SWPT-XSRF-RU";
        private Func<OAuthProvider, String, AuthWebServerClient> _authWebServerClientFactory;

        #endregion

        #region Construction

        public AuthController(Func<OAuthProvider,String,AuthWebServerClient> authWebServerClientFactory)
        {
            Check.Argument.IsNotNull(authWebServerClientFactory, "authWebServerClientFactory");
            _authWebServerClientFactory = authWebServerClientFactory;
        }

        #endregion


        #region Controller Endpoints

        [GET("Logon", IsAbsoluteUrl = true)]
        public ActionResult AuthForm()
        {
            return View(new AuthFormViewModel() { oAuthError = TempData["authError"] as string });
        }


        [GET("OAuth", IsAbsoluteUrl = true)]
        public ActionResult OAuth(string code, string returnUrl, string provider, string error)
        {
            if (!String.IsNullOrWhiteSpace(error))
            {
                //TODO: Move to resource file
                TempData["authError"] =
                    "Access has been denied to your provider account info. For additional features please accept permission request or create a SportsWebPt account";
                return Redirect(String.Format("/logon?ReturnUrl={0}", returnUrl));
            }

            if (string.IsNullOrEmpty(code))
                return PrepareOAuthrRedirect(returnUrl, provider, 0, String.Empty);

            return OAuthCallback();
        }

        [GET("OAuthReg", IsAbsoluteUrl = true)]
        public ActionResult OAuthRegistration(string code, string returnUrl, string provider, string error, int? registrationId, string registrationType)
        {
            if (!String.IsNullOrWhiteSpace(error))
            {
                var redirectTokens = GetRedirectTokens();
                return Redirect(String.Format("/register/{0}?errId=2", redirectTokens[3]));
            }

            if (string.IsNullOrEmpty(code))
                return PrepareOAuthrRedirect(returnUrl, provider, registrationId, registrationType);

            return OAuthCallback();
        }


        private ActionResult PrepareOAuthrRedirect(string returnUrl, string provider, int? registrationId, string registrationType)
        {
            var authWebServerClient = GetOAuthWebServerClient(provider);
            var redirectTokens = String.Format("{0}|{1}|{2}|{3}",
                new object[] {returnUrl, provider, registrationId, registrationType});
            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(redirectTokens.ToUtf8ByteArray(), null));
            Response.Cookies.Add(new HttpCookie(_redirectCookieName)
            {
                Value = encryptedValue,
                Expires = DateTime.Now.AddSeconds(300)
            });

            return authWebServerClient.PrepareRequestUserAuthorization().AsActionResult();
        }

        [GET("Logout", IsAbsoluteUrl = true)]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect(String.Format("/logon?ReturnUrl={0}", "/"));
        }

        [POST("Validate", IsAbsoluteUrl = true)]
        public ActionResult ValidateEmail(String signupEmail)
        {
            var isValid = false;

            if (signupEmail.IsNotNullOrEmpty())
            {
                var existingUser = _userManagementService.GetUser(signupEmail);

                if (existingUser == null)
                    isValid = true;
            }

            return Json(isValid, JsonRequestBehavior.DenyGet);
        }

        [POST("CreateUser", IsAbsoluteUrl = true)]
        public ActionResult CreateNewUser(LoginCommand loginCommand)
        {

            var user = _userManagementService.GetUser(loginCommand.signupEmail);

            if (user != null)
            {
                TempData["authError"] = "Your email address is already in use.";
                return Redirect(String.Format("/logon?ReturnUrl={0}", loginCommand.returnUrl));
            }

            
            //TODO: relying on client side validation right now
            var userId = _userManagementService.AddUser(new User()
            {
                emailAddress = loginCommand.signupEmail,
                userName = loginCommand.username,
                hash = PasswordHashHelper.CreateHash(loginCommand.signupPassword),
                provider = "SWPT"
            });

            FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);

            if (Url.IsLocalUrl(loginCommand.returnUrl))
                return Redirect(loginCommand.returnUrl);

            return Redirect("/");
        }

        [POST("/register/{registrationId}/patient")]
        public ActionResult RegisterPatient(int registrationId, LoginCommand loginCommand)
        {
            //TODO: relying on client side validation right now
            var userId = _userManagementService.RegisterPatient(new User()
            {
                emailAddress = loginCommand.signupEmail,
                userName = loginCommand.username,
                hash = PasswordHashHelper.CreateHash(loginCommand.signupPassword),
                provider = "SWPT"
            }, registrationId);

            if (userId == 0)
                return Redirect(String.Format("/registration/patient?errId=1"));

            FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);

            if (Url.IsLocalUrl(loginCommand.returnUrl))
                return Redirect(loginCommand.returnUrl);

            return Redirect("/");
        }

        [POST("/register/{registrationId}/therapist")]
        public ActionResult RegisterTherapist(int registrationId, LoginCommand loginCommand)
        {
            //TODO: relying on client side validation right now
            var userId = _userManagementService.RegisterTherapist(new User()
            {
                emailAddress = loginCommand.signupEmail,
                userName = loginCommand.username,
                hash = PasswordHashHelper.CreateHash(loginCommand.signupPassword),
                provider = "SWPT"
            }, registrationId);

            if (userId == 0)
                return Redirect(String.Format("/registration/therapist?errId=1"));

            FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);

            if (Url.IsLocalUrl(loginCommand.returnUrl))
                return Redirect(loginCommand.returnUrl);

            return Redirect("/");
        }

        [POST("Login", IsAbsoluteUrl = true)]
        public ActionResult LoginSwptAccount(LoginCommand loginCommand)
        {
            var user = _userManagementService.Auth(loginCommand.userEmail, loginCommand.userPassword);

            if (user == null)
            {
                TempData["authError"] = "Your email address and password do not match, please try again.";
                return Redirect(String.Format("/logon?ReturnUrl={0}", loginCommand.returnUrl));
            }

            return SwptLoginRedirect(loginCommand, user.id);
        }

        #endregion

        #region Private Methods

        private RedirectResult SwptLoginRedirect(LoginCommand loginCommand,int userId)
        {
            FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);

            if (Url.IsLocalUrl(loginCommand.returnUrl))
                return Redirect(loginCommand.returnUrl);

            return Redirect("/");
        }

        private AuthWebServerClient GetOAuthWebServerClient(string provider)
        {
            Check.Argument.IsNotNullOrEmpty(provider, "OAuthProvider");
            var oauthProvider = (OAuthProvider)Enum.Parse(typeof(OAuthProvider), provider, true);

            return _authWebServerClientFactory.Invoke(oauthProvider, Request.Url.GetLeftPart(UriPartial.Path));
        }


        private ActionResult OAuthCallback()
        {
            //HACK: This is a mess
            var redirectTokens = GetRedirectTokens();
            var redirectUri = redirectTokens[0];
            var authWebServerClient = GetOAuthWebServerClient(redirectTokens[1]);
            authWebServerClient.ProcessUserAuthorization(Request);

            var userInfo = authWebServerClient.GetUserInfo();

            if (userInfo != null)
            {
                var registrationId = Convert.ToInt32(redirectTokens[2]);
                var user = new User()
                            {
                                emailAddress = userInfo.EmailAddress,
                                firstName = userInfo.FirstName,
                                lastName = userInfo.LastName,
                                gender = userInfo.Gender,
                                locale = userInfo.Locale,
                                provider = userInfo.Provider.ToString(),
                                providerId = userInfo.ProviderId
                            };

                if (registrationId == 0)
                {
                    var existingUser = _userManagementService.GetUser(userInfo.EmailAddress);
                    var userId = existingUser != null ? existingUser.id : 0;

                    if (userId > 0)
                    {
                        if (!existingUser.provider.Equals(redirectTokens[1], StringComparison.OrdinalIgnoreCase))
                        {
                            //TODO: Move to resource file
                            TempData["authError"] =
                                String.Format(
                                    "The email address used for your {0} account is already being used. Please try another login provider or your SportsWebPt account",
                                    redirectTokens[1]);
                            return Redirect(String.Format("/logon?ReturnUrl={0}", redirectUri));
                        }
                    }
                    else
                        userId = _userManagementService.AddUser(user);

                    FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);
                }
                else
                {
                    var userId = redirectTokens[3].Equals("patient", StringComparison.OrdinalIgnoreCase)
                        ? _userManagementService.RegisterPatient(user, registrationId)
                        : _userManagementService.RegisterTherapist(user, registrationId);

                    if(userId == 0)
                        return Redirect(String.Format("/registration/{0}?errId=1", redirectTokens[3]));

                    FormsAuthentication.SetAuthCookie(Convert.ToString(userId), false);
                }
            }

            if (Url.IsLocalUrl(redirectUri))
                return Redirect(redirectUri);

            return Redirect("/");
        }

        private String[] GetRedirectTokens()
        {
            var cookie = Request.Cookies[_redirectCookieName];

            if (cookie != null
                && !String.IsNullOrEmpty(cookie.Value))
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Set(cookie);

                return Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(cookie.Value), null)).Split('|');
            }

            return new string[] { };
        }

        protected override void Dispose(bool disposing)
        {
            if (_userManagementService != null)
                _userManagementService.Dispose();
        } 
        #endregion
    }
}
