using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Ef.Migrations;
using SportsWebPt.Identity.Core;
using SportsWebPt.Identity.Services.Core;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.MembershipReboot;

namespace SportsWebPt.Identity.Services
{
    public class MembershipRebootUserServiceFactory
    {
        public static IUserService Factory()
        {
            var db = new SportsWebMembershipRebootDatabase(IdentityServerConfigSettings.Instance.PersistanceConnection);
            var repo = new SportsWebUserAccountRepo(db);
            var userAccountService = new UserAccountService<SportsWebUser>(Config, repo);
            var userSvc = new SportsWebPtUserService(userAccountService, db);

            return userSvc;
        }

        public static MembershipRebootConfiguration<SportsWebUser> Config;
        static MembershipRebootUserServiceFactory()
        {

            Config = new MembershipRebootConfiguration<SportsWebUser>
            {
                PasswordHashingIterationCount = 50000,
                AllowLoginAfterAccountCreation = true,
                RequireAccountVerification = false
            };
        }
    }

    public class UserAccountServiceFactory
    {
        public static UserAccountService<SportsWebUser> GetUserAccountService()
        {
            var db = new SportsWebMembershipRebootDatabase(IdentityServerConfigSettings.Instance.PersistanceConnection);
            var repo = new SportsWebUserAccountRepo(db);
            return new UserAccountService<SportsWebUser>(MembershipRebootUserServiceFactory.Config, repo);
        } 
    }


    public class SportsWebPtUserService : MembershipRebootUserService<SportsWebUser>
    {

        public SportsWebPtUserService(UserAccountService<SportsWebUser> userAccountService, IDisposable cleanup)
            : base(userAccountService, cleanup)
        {}


        public override Task<AuthenticateResult> AuthenticateExternalAsync(ExternalIdentity externalUser)
        {
            if (externalUser == null)
                throw new ArgumentNullException("externalUser");

            var acct = userAccountService.GetByLinkedAccount(externalUser.Provider, externalUser.ProviderId);

            if (acct == null)
            {
                var createAccountResult = ProcessNewExternalAccountAsync(externalUser.Provider, externalUser.ProviderId,
                     externalUser.Claims).Result;

                return Task.FromResult(new AuthenticateResult("/core/register/external", externalUser));
            }

            return ProcessExistingExternalAccountAsync(acct.ID, externalUser.Provider,
                externalUser.ProviderId, GetClaimsFromAccount(acct));

        }
    }
}