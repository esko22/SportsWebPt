using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Common.Logging;
using SportsWebPt.Platform.Core;
using SportsWebPt.Platform.ServiceImpl.Operations;
using SportsWebPt.Platform.ServiceImpl.Services;

namespace SportsWebPt.Platform.ServiceHost
{
    public class AppHost : AppHostBase
    {
        #region Fields

        private PlatformServiceConfiguration _configuration;

#if DEBUG
        private const Feature DisableFeatures = Feature.Soap | Feature.Markdown | Feature.Soap11 | Feature.Soap12 | Feature.Csv | Feature.CustomFormat;
#else
         private const Feature DisableFeatures = Feature.Soap | Feature.Markdown | Feature.Soap11 | Feature.Soap12 | Feature.Csv | Feature.CustomFormat | Feature.Metadata;
#endif

        #endregion



        #region Construction

        public AppHost()
            : base("SportsWebPt Platform Services", new[] { typeof(UserService).Assembly, typeof(SwaggerResourceService).Assembly })
        { } 

        #endregion


        #region Methods
        
        public override void Configure(Container container)
        {
            JsConfig.EmitCamelCaseNames = false;
            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.ExcludeTypeInfo = true;
            JsConfig.IncludeNullValues = false;

            //Change the default ServiceStack configuration
            SetConfig(new EndpointHostConfig
            {
                EnableFeatures = Feature.All.Remove(DisableFeatures),
                DefaultContentType = ContentType.Json,
#if DEBUG
                DebugMode = true, //Show StackTraces in responses in development
#endif
            });

            // Unregistered path handler for custom 404s
#if !DEBUG

            //put in a default 404 handler in release mode
            CatchAllHandlers.Add(((httpMethod, pathInfo, filePath) =>
            {
                if (pathInfo.Contains("favicon"))
                    return new DefaultHttpHandler();

                return new CustomNotFoundHttpHandler();
            }));
#endif

            // Remove the x-powered-by servicestack header
            Config.GlobalResponseHeaders.Clear();

            // NOTE: uncomment this to disable the HTML display
            //// Remove the HTML display for routes
            //ContentTypeFilters.ClearCustomFilters();

            // add in stuff for every page to allow cross-site calls (helps AJAX clients)
            Config.GlobalResponseHeaders["Access-Control-Allow-Origin"] = "*";
            Config.GlobalResponseHeaders["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
            Config.GlobalResponseHeaders["Access-Control-Allow-Headers"] = "Content-Type";

            LogManager.LoggerFactory = new NLogLoggerFactory();
            _configuration = PlatformServiceConfiguration.Instance;

            ConfigureContainer(container);
            BuildRoutes();
            Container.Resolve<ApiDocumentGenerator>().Generate();
        } 

        private void BuildRoutes()
        {
            Routes
                .Add<UserRequest>("user");

        }

        private void ConfigureContainer(Container container)
        {

            container.Register<ApiDocumentGenerator>(c => new SwaggerApiDocumentGenerator(_configuration.ApiDocumentAssemblies)).ReusedWithin(ReuseScope.Container);
            container.Register<IBaseApiConfig>(c => PlatformServiceConfiguration.Instance).ReusedWithin(ReuseScope.Container);

        }

        #endregion
    }
}