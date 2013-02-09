using System.Net;
using System.Web;

using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints.Support;
using EndpointExtensions = ServiceStack.WebHost.Endpoints.Extensions;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    public class CustomNotFoundHttpHandler
      : IServiceStackHttpHandler, IHttpHandler
    {
        public void ProcessRequest(IHttpRequest request, IHttpResponse response, string operationName)
        {
            if (request.HttpMethod.ToUpper() == "OPTIONS")
            {
                // TODO : unfortunately this is not working for the root path as servicestack reserves that for the meta redirect.

                response.StatusCode = (int)HttpStatusCode.OK;
                // add in stuff to allow cross-site calls (helps AJAX clients)
                response.AddHeader("Access-Control-Allow-Origin", "*");
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
                response.Close();
            }
            else
            {
                var dto = new ErrorResponse(TextResources.Error404, null);
                var json = JsonSerializer.SerializeToString(dto);
                response.ContentType = ContentType.Json;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Write(json);
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequest(
                new EndpointExtensions.HttpRequestWrapper(typeof(RequestInfo).Name, context.Request),
                new EndpointExtensions.HttpResponseWrapper(context.Response),
                typeof(ErrorResponse).Name
                );
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
