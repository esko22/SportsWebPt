using SportsWebPt.Identity.Services;
using SportsWebPt.Identity.Services.Core;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace Thinktecture.IdentityServer.Host.Config
{
    public class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            return new IdentityServerServiceFactory
            {
                UserService = Registration<IUserService>.RegisterFactory(
                    () => MembershipRebootUserServiceFactory.Factory()),
                ScopeStore = Registration.RegisterFactory<IScopeStore>(() => new InMemoryScopeStore(Scopes.Get())),
                ClientStore = Registration.RegisterFactory<IClientStore>(() => new InMemoryClientStore(Clients.Get())),
                ViewService = Registration.RegisterType<IViewService>(typeof(ViewService))
            };
        }
    }
}