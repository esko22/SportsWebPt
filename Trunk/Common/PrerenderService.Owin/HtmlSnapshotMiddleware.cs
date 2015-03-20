using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using PrerenderService.Configuration;
using PrerenderService.Service;
using SportsWebPt.Common.Logging;

namespace PrerenderService.Owin
{
    public class HtmlSnapshotMiddleware : OwinMiddleware
    {
        private readonly ILog _logger = LogManager.GetCommonLogger();

        public HtmlSnapshotMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            PrerenderConfig config = PrerenderConfig.GetCurrent();
            var requestParams = new RequestParams
                                    {
                                        RequestUri = context.Request.Uri,
                                        UserAgent = context.Request.Headers["user-agent"]
                                    };
            if (Utility.IsRequestShouldBePrerendered(requestParams, config))
            {
                _logger.Info(String.Format("Crawl Request Submitted: {0} - {1}", requestParams.RequestUri, requestParams.UserAgent));

                var rendererConfig = new PrerenderServiceConfiguration(config);
                var renderer = new SnapshotRenderer(rendererConfig);


                string snapshotUrl = Utility.GetSnapshotUrl(context.Request.Uri);
                //render page html
                PrerenderResult response;
                try
                {
                    response = await renderer.RenderPage(snapshotUrl);
                }
                catch (WebException e)
                {
                    context.Response.Write(
                        string.Format("Failed to prerender request '{0}'. Error message: '{1}'. Stack trace: '{2}'",
                                      snapshotUrl, e.Message, e.StackTrace));
                    context.Response.StatusCode = 500;
                    _logger.Error("Prerender Exception", e);
                    return;
                }

                await context.Response.WriteAsync(response.Content);
                context.Response.ContentType = "text/html";
                context.Response.StatusCode = (int) response.StatusCode;
            }
            else
            {
                await Next.Invoke(context);
            }
        }
    }
}