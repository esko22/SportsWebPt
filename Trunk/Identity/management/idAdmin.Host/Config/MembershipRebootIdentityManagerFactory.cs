using System.Data.Entity;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using SportsWebPt.Identity.Core;
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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportsWebMembershipRebootDatabase, SportsWebMigrationConfig>());
            var db = new SportsWebMembershipRebootDatabase(connString);
            var userRepo = new SportsWebUserAccountRepo(db);
            userRepo.QueryFilter = RelationalUserAccountQuery<SportsWebUser>.Filter;
            userRepo.QuerySort = RelationalUserAccountQuery<SportsWebUser>.Sort;
            var userSvc = new UserAccountService<SportsWebUser>(MRConfig.config, userRepo);
            var groupRepo = new DbContextGroupRepository<SportsWebMembershipRebootDatabase, RelationalGroup>(db);
            var groupSvc = new GroupService<RelationalGroup>(MRConfig.config.DefaultTenant, groupRepo);
            var idMgr = new MembershipRebootIdentityManagerService<SportsWebUser, RelationalGroup>(userSvc, groupSvc);
            
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

            return new DisposableIdentityManagerService(idMgr, db);
        }
    }

}