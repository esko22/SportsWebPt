using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Models;

namespace Thinktecture.IdentityServer.Host.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
            {

                ////////////////////////
                // identity scopes
                ////////////////////////

                Scope.OpenId,
                Scope.Profile,
                Scope.Email,
                Scope.OfflineAccess,

                new Scope
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "Your organizational roles",
                    Type = ScopeType.Identity,

                    Claims = new[]
                    {
                        new ScopeClaim(Constants.ClaimTypes.Role, alwaysInclude: true),
                    }
                },

                ////////////////////////
                // resource scopes
                ////////////////////////
                new Scope
                {
                    Name = "userDetail",
                    DisplayName = "Username",
                    Description = "What you call yourself",
                    Type = ScopeType.Identity,
                    Claims = new []
                    {
                        new ScopeClaim("username", true) 
                    }
                },
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read data",
                    Type = ScopeType.Resource,
                    Emphasize = false,
                },
                new Scope
                {
                    Name = "write",
                    DisplayName = "Write data",
                    Type = ScopeType.Resource,
                    Emphasize = true,
                },
                new Scope
                {
                    Name = "idmgr",
                    DisplayName = "IdentityManager",
                    Type = ScopeType.Resource,
                    Emphasize = true,

                    Claims = new[]
                    {
                        new ScopeClaim(Constants.ClaimTypes.Name),
                        new ScopeClaim(Constants.ClaimTypes.Role)
                    }
                }
            };
        }
    }
}