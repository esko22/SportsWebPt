using System.Configuration;
using SportsWebPt.Common.Logging;
using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Common.Web.Auth;
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
            var baseClientSettings = new BaseServiceStackClientSettings()
                {
                    BaseUri = ConfigurationManager.AppSettings["platofrmServiceUri"]
                };

            kernel.Bind<IUserManagementService>()
                  .To<UserManagementService>().InRequestScope()
                  .WithConstructorArgument("clientSettings", baseClientSettings);


            kernel.Bind<Func<OAuthProvider, String, AuthWebServerClient>>().ToConstant(
                new Func<OAuthProvider, String, AuthWebServerClient>((oauthProvider, uri) =>
                    {
                        switch (oauthProvider)
                        {
                            case OAuthProvider.Facebook:
                                return new FacebookAuthWebClient("440254562722232", "e23388db22fdf5330b7459b7697fa13a");
                            case OAuthProvider.Google:
                                return new GoogleAuthWebClient("136219353860.apps.googleusercontent.com",
                                                               "SNzL1wJ1Pf_EdiwYrXh0kvtN",
                                                               uri);
                            default:
                                throw new ArgumentException(
                                    String.Format("{0} OAuth Provider does not have a web client", oauthProvider));

                        }
                    })
                );

        }        
    }
}
