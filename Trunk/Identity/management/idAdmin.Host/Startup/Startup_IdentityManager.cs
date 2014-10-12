
using Microsoft.Owin;
using Owin;
using SportsWebPt.Identity.Admin;
using Thinktecture.IdentityManager;

[assembly: OwinStartup("IdentityManager", typeof(Startup_IdentityManager))]
namespace SportsWebPt.Identity.Admin
{
    public class Startup_IdentityManager
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new MembershipRebootIdentityManagerFactory("MembershipReboot");
            var config = new IdentityManagerConfiguration
            {
                IdentityManagerFactory = factory.Create,
                SecurityMode = SecurityMode.LocalMachine
            };

            app.UseIdentityManager(config);
        }

    }
}