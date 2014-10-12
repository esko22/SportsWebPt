using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.Owin;
using Nancy.ViewEngines;


namespace SportsWebPt.Platform.Web.Application
{
    public class IndexModule : NancyModule
    {
        #region Constructor
        
        public IndexModule()
        {
            Get["/"] = _ => View["index", Context.BuildIndexModel()];
        } 

        #endregion
    }

    public class DefaultRouteHandler : IStatusCodeHandler
    {
        private readonly IViewRenderer _viewRenderer;

        public DefaultRouteHandler(IViewRenderer viewRenderer)
        {
            _viewRenderer = viewRenderer;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode,
                                      NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = _viewRenderer.RenderView(context, "index", context.BuildIndexModel());
            response.StatusCode = HttpStatusCode.OK;
            
            context.Response = response;
        }
    }

    public static class PortalHelpers
    {
        public static dynamic BuildIndexModel(this NancyContext context)
        {
            var environment = context.GetOwinEnvironment();
            var user = (ClaimsPrincipal)environment["server.User"];
            var tokenClaim = user.FindFirst("auth_time");

            return new { title = "Accessible Physical Therapy", authTime = tokenClaim == null ? "" : tokenClaim.Value };
        }
    }
}