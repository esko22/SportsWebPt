using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.Owin;
using Nancy.Security;
using Nancy.ViewEngines;
using SportsWebPt.Platform.Web.Core;


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
            //TODO: make this configurable list.. tied to bundling paths, other static assets
            if (context.Request.Path != "/content/css" 
                && context.Request.Path != "/js/post" 
                && context.Request.Path != "/js/pre"
                && context.Request.Path != "/sitemap.xml"
                && context.Request.Path != "/robots.txt")
            {
                var response = _viewRenderer.RenderView(context, "index", context.BuildIndexModel());
                response.StatusCode = HttpStatusCode.OK;

                context.Response = response;
            }
        }
    }

    public static class PortalHelpers
    {
        public static dynamic BuildIndexModel(this NancyContext context)
        {
            return new
            {
                title = "Accessible Physical Therapy",
                authRedirectPath = WebPlatformConfigSettings.Instance.CallbackUri,
                authorityUrl = WebPlatformConfigSettings.Instance.AuthorityUri
            };
        }
    }
}