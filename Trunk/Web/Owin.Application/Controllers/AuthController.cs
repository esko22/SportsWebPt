using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Microsoft.Owin.Security.Infrastructure;

namespace SportsWebPt.Platform.Web.Application
{
    [Authorize]
    public class AuthController : ApiController
    {

        #region Construction

        public AuthController()
        {
            
        }

        #endregion

        [HttpGet]
        [Route("auth")]
        public object Get(String returnUrl)
        {
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

        [HttpGet]
        [Route("register")]
        public object Register(String returnUrl)
        {
            var subjectId = User.GetSubjectId();
            //do some logic to create a linked account with service

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority) + returnUrl);
            return response;
        }
    }
}