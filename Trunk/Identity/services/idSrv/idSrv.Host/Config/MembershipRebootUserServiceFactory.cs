using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Ef.Migrations;
using SportsWebPt.Identity.Services.Core;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Authentication;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Plumbing;
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


        public override Task<AuthenticateResult> AuthenticateExternalAsync(ExternalIdentity externalUser)
        {
            if (externalUser == null)
                throw new ArgumentNullException("externalUser");

            var acct = userAccountService.GetByLinkedAccount(externalUser.Provider.Name, externalUser.ProviderId);

            if (acct == null)
            {
                var createAccountResult = ProcessNewExternalAccountAsync(externalUser.Provider.Name, externalUser.ProviderId,
                     externalUser.Claims).Result;

                return Task.FromResult(new AuthenticateResult("/core/register/external", externalUser));
            }

            return ProcessExistingExternalAccountAsync(acct.ID, externalUser.Provider.Name,
                externalUser.ProviderId, GetClaimsFromAccount(acct));

        }
    }
}