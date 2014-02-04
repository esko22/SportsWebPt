using System.Data.Entity;
using System.Web;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

using Funq;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Common.Logging;
using SportsWebPt.Platform.Core;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceImpl;
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
        {
            LogManager.LoggerFactory = new NLogLoggerFactory();
        } 

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

            ServiceContentMaps.CreateContentMaps();

            _configuration = PlatformServiceConfiguration.Instance;

            ConfigureContainer(container);
            BuildRoutes();
            BoostrapEf();

        } 

        private void BuildRoutes()
        {
            // routes all moved over to attrbiutes on operations
        }

        public override IServiceRunner<TRequest> CreateServiceRunner<TRequest>(ActionContext actionContext)
        {
            return new LoggingServiceRunner<TRequest>(this,actionContext);
        }


        private void ConfigureContainer(Container container)
        {
            container.Register<IBaseApiConfig>(c => PlatformServiceConfiguration.Instance).ReusedWithin(ReuseScope.Container);
            container.Register<RepositoryFactory>(c => new PlatformRepositoryFactory())
                     .ReusedWithin(ReuseScope.Container);
            container.Register<IRepositoryProvider>(c => new PlatformRepositoryProvider(c.Resolve<RepositoryFactory>()))
                     .ReusedWithin(ReuseScope.Request);
            container.Register<IUserUnitOfWork>(c => new UserUnitOfWork(c.Resolve<IRepositoryProvider>())).ReusedWithin(ReuseScope.Request);
            container.Register<ISkeletonUnitOfWork>(c => new SkeletonUnitOfWork(c.Resolve<IRepositoryProvider>())).ReusedWithin(ReuseScope.Request);
            container.Register<IDiffDiagUnitOfWork>(c => new DiffDiagUnitOfWork(c.Resolve<IRepositoryProvider>())).ReusedWithin(ReuseScope.Request);
            container.Register<IResearchUnitOfWork>(c => new ResearchUnitOfWork(c.Resolve<IRepositoryProvider>())).ReusedWithin(ReuseScope.Request);
            container.Register<ILookupUnitOfWork>(c => new LookupUnitOfWork(c.Resolve<IRepositoryProvider>()))
                     .ReusedWithin(ReuseScope.Request);
        }

        private void BoostrapEf()
        {
            var seeder = new PlatformDbDefaultSeeder();
            var dbConextInitializer = new PlatformDbCreateInitializer {Seeder = seeder};
            Database.SetInitializer(dbConextInitializer);
        }

        #endregion
    }
}