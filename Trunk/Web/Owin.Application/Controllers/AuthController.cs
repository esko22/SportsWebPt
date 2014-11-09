using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace SportsWebPt.Platform.Web.Application
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("test")]
        public dynamic Signout()
        {
            return "Some fucking shite";
        }
    }
}