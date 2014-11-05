using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    [Authorize]
    public class AuthController : ApiController
    {

        #region Fields

        private readonly IUserManagementService _userManagementService;

        #endregion

        #region Construction

        public AuthController(IUserManagementService userManagementService)
        {
            Check.Argument.IsNotNull(userManagementService, "UserManagementService");
            _userManagementService = userManagementService;
        }

        #endregion

        [HttpGet]
        [Route("auth")]
        public object Get(String returnUrl)
        {
            //TODO:
            //can maybe looking at exposing a service on web to create service user and build it into the post registration process 
            //on the identity server so we don't have to do this check on every auth attempt
            if (String.IsNullOrEmpty(User.GetServiceAccount()))
            {
                _userManagementService.CreateServiceAccount(User.GetSubjectId());
                Request.GetOwinContext().Authentication.SignIn(((ClaimsPrincipal)User).Identities.ToArray());
            }
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority) + returnUrl);
            return response;
        }

        [HttpGet]
        [Route("signout")]
        public void Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
        }
     
    }
}