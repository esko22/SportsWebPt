using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace PrerenderService.Owin
{
    public class PrerenderIOMiddleware : OwinMiddleware
    {
        public PrerenderIOMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            //PrerenderConfig config = PrerenderConfig.GetCurrent();
            //var requestParams = new RequestParams
            //{
            //    RequestUri = context.Request.Uri,
            //    UserAgent = context.Request.Headers["user-agent"]
            //};
            //if (Utility.IsRequestShouldBePrerendered(requestParams, config))
            //{
            //    var rendererConfig = new PrerenderServiceConfiguration(config);
            //    var renderer = new SnapshotRenderer(rendererConfig);


            //    string snapshotUrl = Utility.GetSnapshotUrl(context.Request.Uri);
            //    //render page html
            //    PrerenderResult response;
            //    try
            //    {
            //        response = await renderer.RenderPage(snapshotUrl);
            //    }
            //    catch (WebException e)
            //    {
            //        context.Response.Write(
            //            string.Format("Failed to prerender request '{0}'. Error message: '{1}'. Stack trace: '{2}'",
            //                          snapshotUrl, e.Message, e.StackTrace));
            //        context.Response.StatusCode = 500;
            //        return;
            //    }

            //    await context.Response.WriteAsync(response.Content);
            //    context.Response.ContentType = "text/html";
            //    context.Response.StatusCode = (int)response.StatusCode;
            //}
            //else
            //{
                await Next.Invoke(context);
            //}
        }
    }

    public static class PrerenderIOExtentions
    {
        /// <summary>
        ///     Adds snapshot middleware, that intercepts all request and generate
        ///     html layout for search machines.
        /// </summary>
        public static void UsePrerenderer(this IAppBuilder builder)
        {
            builder.Use<PrerenderIOMiddleware>();
        }
    }
}
