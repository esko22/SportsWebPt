using System.Data.Entity;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using SportsWebPt.Identity.Core;
using Thinktecture.IdentityManager;
using Thinktecture.IdentityManager.Configuration;
using Thinktecture.IdentityManager.MembershipReboot;

namespace SportsWebPt.Identity.Admin
{
    public static class MembershipRebootIdentityManagerServiceExtensions
    {
        public static void Configure(this IdentityManagerServiceFactory factory, string connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportsWebMembershipRebootDatabase, SportsWebMigrationConfig>());

            factory.IdentityManagerService = new Registration<IIdentityManagerService, CustomIdentityManagerService>();
            factory.Register(new Registration<SportsWebUserAccountService>());
            factory.Register(new Registration<SportsWebGroupService>());
            factory.Register(new Registration<SportsWebUserRepository>());
            factory.Register(new Registration<SportsWebGroupRepository>());
            factory.Register(new Registration<SportsWebMembershipRebootDatabase>(resolver => new SportsWebMembershipRebootDatabase(connectionString)));
            factory.Register(new Registration<SportsWebMembershipRebootConfig>(new SportsWebMembershipRebootConfig()));
        }
    }

    public class CustomIdentityManagerService : MembershipRebootIdentityManagerService<SportsWebUser, SportsWebGroup>
    {
        public CustomIdentityManagerService(SportsWebUserAccountService userSvc, SportsWebGroupService groupSvc)
            : base(userSvc, groupSvc)
        {
        }
    }


}