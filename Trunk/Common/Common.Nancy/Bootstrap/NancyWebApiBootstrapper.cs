using System.Web.Http;

using Microsoft.Owin.Extensions;

using Nancy;
using Nancy.Bootstrapper;
using Nancy.Owin;
using Nancy.TinyIoc;
using Nancy.Security;

using Owin;
using SportsWebPt.Common.Logging;
using SportsWebPt.Common.Utilities;

namespace SportsWebPt.Common.Nancy
{
    public class NancyWebApiBootstrapper : DefaultNancyBootstrapper
    {
         #region Fields

        private readonly TinyIoCContainer _tinyIoCContainer;
        protected readonly HttpConfiguration _httpConfiguration;

        #endregion

        #region Construction

        public NancyWebApiBootstrapper() 
            : this(new TinyIoCContainer(), new HttpConfiguration()) 
        {
        }

        public NancyWebApiBootstrapper(HttpConfiguration httpConfiguration)
            : this(new TinyIoCContainer(), httpConfiguration)
        {}

        public NancyWebApiBootstrapper(TinyIoCContainer tinyIoCContainer, HttpConfiguration httpConfiguration)
        {
            Check.Argument.IsNotNull(tinyIoCContainer,"TinyIOC Cotainer");
            Check.Argument.IsNotNull(httpConfiguration, "WebApi Http Configuration");

            _tinyIoCContainer = tinyIoCContainer;
            _httpConfiguration = httpConfiguration;

            _httpConfiguration.DependencyResolver = new NancyTinyIocApiResolver(tinyIoCContainer);

            LogManager.LoggerFactory = new NLogLoggerFactory();

        }

        #endregion

        #region Properties

        protected override TinyIoCContainer GetApplicationContainer()
        {
            return _tinyIoCContainer;
        }

        protected HttpConfiguration HttpConfiguration { get { return _httpConfiguration; } }

        #endregion

        #region Methods
        
        protected virtual void ConfigureWebApi()
        {
            _httpConfiguration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            ConfigureWebApi();
            BuildBundles();

            base.ApplicationStartup(container, pipelines);
        }

        protected virtual void BuildBundles() { }

        public virtual void Configuration(IAppBuilder appBuilder)
        {
            ConfigureAuthMiddleware(appBuilder);

            appBuilder.UseWebApi(HttpConfiguration);

            //Nancy route handling is kinda greedy.
            appBuilder.UseNancy(new NancyOptions() { Bootstrapper = this});

            //, PerformPassThrough = context => context.Response.StatusCode == HttpStatusCode.NotFound 

            //IIS's native static file module is very greedy.
            //http://katanaproject.codeplex.com/discussions/470920
            appBuilder.UseStageMarker(PipelineStage.MapHandler);
        }

        protected virtual void ConfigureAuthMiddleware(IAppBuilder appBuilder) { }
       
        #endregion

    }
}
