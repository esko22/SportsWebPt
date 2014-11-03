using System.Data.Entity;
using SportsWebPt.Identity.Core;
using SportsWebPt.Identity.Services;
using SportsWebPt.Identity.Services.Core;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.EntityFramework;
using Thinktecture.IdentityServer.Core.Services;

namespace Thinktecture.IdentityServer.Host.Config
{
    public class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportsWebMembershipRebootDatabase, SportsWebMigrationConfig>());

            var svcFactory = new ServiceFactory(IdentityServerConfigSettings.Instance.ConfigConnection);
            svcFactory.ConfigureClients(Clients.Get());
            svcFactory.ConfigureScopes(Scopes.Get());

            return new IdentityServerServiceFactory
            {
                UserService = Registration.RegisterFactory(MembershipRebootUserServiceFactory.Factory),
                ScopeStore = Registration.RegisterFactory(svcFactory.CreateScopeStore),
                ClientStore = Registration.RegisterFactory(svcFactory.CreateClientStore),
                AuthorizationCodeStore = Registration.RegisterFactory(svcFactory.CreateAuthorizationCodeStore),
                TokenHandleStore = Registration.RegisterFactory(svcFactory.CreateTokenHandleStore),
                ConsentStore = Registration.RegisterFactory(svcFactory.CreateConsentStore),
                RefreshTokenStore = Registration.RegisterFactory(svcFactory.CreateRefreshTokenStore),
                ViewService = Registration.RegisterType<IViewService>(typeof(ViewService))
            };
        }

    }
}