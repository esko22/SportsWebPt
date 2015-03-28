using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using SportsWebPt.Common.Logging;

namespace PrerenderService.Service
{
    internal class WebClient
    {
        private readonly IPrerenderServiceConfiguration _config;
        private readonly ILog _logger = LogManager.GetCommonLogger();

        public WebClient(IPrerenderServiceConfiguration config)
        {
            _config = config;
        }

        public async Task<WebResponse> Get(string uri)
        {
            //TODO: HACK - This is in place right now because the https redirect at the LB 
            //forces the request to come in as http instead of https. The http request returns a 301 which is correct
            uri = uri.Replace("http", "https");
            string serviceUrl = _config.ServiceUrl.EndsWith("/") ? _config.ServiceUrl : _config.ServiceUrl + "/";
            var webRequest = (HttpWebRequest) WebRequest.Create(serviceUrl + uri);

            var token = System.Environment.GetEnvironmentVariable("Prerender-Token") ?? ConfigurationManager.AppSettings["Prerender-Token"];
            if (!string.IsNullOrEmpty(token))
                webRequest.Headers.Add("X-Prerender-Token", token);

            webRequest.Method = "GET";
            if (!string.IsNullOrEmpty(_config.ProxyUrl))
            {
                webRequest.Proxy = new WebProxy(_config.ProxyUrl);
            }

            _logger.Info(String.Format("PrerenderIO Request To: {0}", webRequest.RequestUri));

            return await webRequest.GetResponseAsync().ConfigureAwait(false);
        }
    }
}