using System.Configuration;
using SportsWebPt.Common.Logging;
using SportsWebPt.Common.Web.Auth;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SportsWebPt.Platform.Web.Application.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(SportsWebPt.Platform.Web.Application.App_Start.NinjectWebCommon), "Stop")]
namespace SportsWebPt.Platform.Web.Application.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            LogManager.LoggerFactory = new NLogLoggerFactory();
            ServicesContentMaps.CreateContentMaps();

            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {

            //kernel.Bind<IUserManagementService>()
            //      .To<UserManagementService>().InRequestScope()
            //      .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<IExamineService>()
            //      .To<ExamineService>().InRequestScope()
            //      .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<IResearchService>()
            //      .To<ResearchService>().InRequestScope()
            //      .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<IAdminService>()
            //      .To<AdminService>().InRequestScope()
            //      .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<IClinicService>()
            //    .To<ClinicService>().InRequestScope()
            //    .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<ITherapistService>()
            //    .To<TherapistService>().InRequestScope()
            //    .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<ILookupService>()
            //      .To<LookupService>().InRequestScope()
            //      .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<IEpisodeService>()
            //      .To<EpisodeService>().InRequestScope()
            //      .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<ISessionService>()
            //      .To<SessionService>().InRequestScope()
            //      .WithConstructorArgument("clientSettings", WebPlatformConfigSettings.Instance.ServiceStackClientSettings);

            //kernel.Bind<Func<OAuthProvider, String, AuthWebServerClient>>().ToConstant(
            //    new Func<OAuthProvider, String, AuthWebServerClient>((oauthProvider, uri) =>
            //        {
            //            switch (oauthProvider)
            //            {
            //                case OAuthProvider.Facebook:
            //                    return new FacebookAuthWebClient(WebPlatformConfigSettings.Instance.FacebookClientKey, WebPlatformConfigSettings.Instance.FacebookClientSecret);
            //                case OAuthProvider.Google:
            //                    return new GoogleAuthWebClient(WebPlatformConfigSettings.Instance.GoogleClientKey, WebPlatformConfigSettings.Instance.GoogleClientSecret,uri);
            //                default:
            //                    throw new ArgumentException(
            //                        String.Format("{0} OAuth Provider does not have a web client", oauthProvider));

            //            }
            //        })
            //    );

        }        
    }
}
