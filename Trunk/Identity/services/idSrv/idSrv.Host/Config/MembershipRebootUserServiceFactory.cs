using System;
using System.Data.Entity;

using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Ef.Migrations;
using SportsWebPt.Identity.Services.Core;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.MembershipReboot;

namespace SportsWebPt.Identity.Services
{
    public class MembershipRebootUserServiceFactory
    {
        public static IUserService Factory()
        {
            var repo = new DefaultUserAccountRepository(IdentityServerConfigSettings.Instance.PersistanceConnection);
            var userAccountService = new UserAccountService(Config, repo);
            var userSvc = new SportsWebPtUserService(userAccountService, repo);

            return userSvc;
        }

        public static MembershipRebootConfiguration Config;
        static MembershipRebootUserServiceFactory()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultMembershipRebootDatabase, Configuration>());

            Config = new MembershipRebootConfiguration
            {
                PasswordHashingIterationCount = 50000,
                AllowLoginAfterAccountCreation = true,
                RequireAccountVerification = false
            };
        }
    }

    public class UserAccountServiceFactory
    {
        public static UserAccountService<UserAccount> GetUserAccountService()
        {
            var repo = new DefaultUserAccountRepository(IdentityServerConfigSettings.Instance.PersistanceConnection);
            return new UserAccountService(MembershipRebootUserServiceFactory.Config, repo);
        } 
    }


    public class SportsWebPtUserService : MembershipRebootUserService<UserAccount>
    {

        public SportsWebPtUserService(UserAccountService<UserAccount> userAccountService, IDisposable cleanup)
            : base(userAccountService, cleanup)
        {}
    }
}