
using Microsoft.Owin;
using Owin;
using SportsWebPt.Identity.Admin;
using Thinktecture.IdentityManager.Configuration;

[assembly: OwinStartup("IdentityManager", typeof(Startup_IdentityManager))]
namespace SportsWebPt.Identity.Admin
{
    public class Startup_IdentityManager
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new IdentityManagerServiceFactory();
            factory.Configure("MembershipReboot");
            var options = new IdentityManagerOptions
            {
                Factory = factory,
                SecurityMode = SecurityMode.LocalMachine
            };

            app.UseIdentityManager(options);
        }

    }
}