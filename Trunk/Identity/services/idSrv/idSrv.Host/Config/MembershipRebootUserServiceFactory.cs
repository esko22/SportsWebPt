﻿using System;

using BrockAllen.MembershipReboot;

using SportsWebPt.Identity.Core;
using SportsWebPt.Identity.Services.Core;

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
            var userAccountService =
                new UserAccountService<SportsWebUser>(
                    new SportsWebMembershipRebootConfig(IdentityServerConfigSettings.Instance.DefaultClientUri,
                        IdentityServerConfigSettings.Instance.PublicHostName), repo);
            var userSvc = new SportsWebPtUserService(userAccountService, db);

            return userSvc;
        }
    }

    public class UserAccountServiceFactory
    {
        public static UserAccountService<SportsWebUser> GetUserAccountService()
        {
            var db = new SportsWebMembershipRebootDatabase(IdentityServerConfigSettings.Instance.PersistanceConnection);
            var repo = new SportsWebUserAccountRepo(db);
            return
                new UserAccountService<SportsWebUser>(
                    new SportsWebMembershipRebootConfig(IdentityServerConfigSettings.Instance.DefaultClientUri,
                        IdentityServerConfigSettings.Instance.PublicHostName), repo);
        } 
    }


    public class SportsWebPtUserService : MembershipRebootUserService<SportsWebUser>
    {

        public SportsWebPtUserService(UserAccountService<SportsWebUser> userAccountService, IDisposable cleanup)
            : base(userAccountService, cleanup)
        {}

        //TODO: commenting out for now, don't need any additional info for external openid providers at this time
        //public override Task<AuthenticateResult> AuthenticateExternalAsync(ExternalIdentity externalUser)
        //{
        //    if (externalUser == null)
        //        throw new ArgumentNullException("externalUser");

        //    var acct = userAccountService.GetByLinkedAccount(externalUser.Provider, externalUser.ProviderId);

        //    if (acct == null)
        //    {
        //        var createAccountResult = ProcessNewExternalAccountAsync(externalUser.Provider, externalUser.ProviderId,
        //             externalUser.Claims).Result;

        //        return Task.FromResult(new AuthenticateResult("/core/register/external", externalUser));
        //    }

        //    return ProcessExistingExternalAccountAsync(acct.ID, externalUser.Provider,
        //        externalUser.ProviderId, GetClaimsFromAccount(acct));

        //}
    }
}