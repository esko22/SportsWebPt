using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;
using Thinktecture.IdentityManager;
using Thinktecture.IdentityManager.MembershipReboot;

namespace SportsWebPt.Identity.Admin
{
    public class MembershipRebootIdentityManagerFactory
    {
        string connString;
        public MembershipRebootIdentityManagerFactory(string connString)
        {
            this.connString = connString;
        }
        
        public IIdentityManagerService Create()
        {

            var userRepo = new DefaultUserAccountRepository(connString)
            {
                QueryFilter = RelationalUserAccountQuery<RelationalUserAccount>.Filter,
                QuerySort = RelationalUserAccountQuery<RelationalUserAccount>.Sort
            };

            var userSvc = new UserAccountService<RelationalUserAccount>(MRConfig.config, userRepo);
            var groupRepo = new DefaultGroupRepository();
            var groupSvc = new GroupService<RelationalGroup>(MRConfig.config.DefaultTenant, groupRepo);

            MembershipRebootIdentityManagerService<RelationalUserAccount, RelationalGroup> idMgr = null;
            idMgr = new MembershipRebootIdentityManagerService<RelationalUserAccount, RelationalGroup>(userSvc, userRepo, groupSvc, groupRepo);
            
            // uncomment to allow additional properties mapped to claims
            //idMgr = new MembershipRebootIdentityManagerService<CustomUser, CustomGroup>(userSvc, userRepo, groupSvc, groupRepo, () =>
            //{
            //    var meta = idMgr.GetStandardMetadata();
            //    meta.UserMetadata.UpdateProperties =
            //        meta.UserMetadata.UpdateProperties.Union(
            //            new PropertyMetadata[] { 
            //                idMgr.GetMetadataForClaim(Constants.ClaimTypes.Name, "Name")
            //            }
            //        );
            //    return Task.FromResult(meta);
            //});

            return new DisposableIdentityManagerService(idMgr, userRepo);
        }
    }
}