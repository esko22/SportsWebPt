using System.Configuration;
using SportsWebPt.Common.ServiceStackClient;
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
        }        
    }
}
