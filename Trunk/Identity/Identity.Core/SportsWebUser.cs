using System;

using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;

namespace SportsWebPt.Identity.Core
{
    public class SportsWebUser : RelationalUserAccount
    {
        #region Properties

        public virtual string ServiceAccount { get; set; }

        #endregion
    }

   
    public class SportsWebUserAccountService : UserAccountService<SportsWebUser>
    {
        public SportsWebUserAccountService(SportsWebMembershipRebootConfig config, SportsWebUserRepository repo)
            : base(config, repo)
        {
        }
    }

    public class SportsWebUserRepository : DbContextUserAccountRepository<SportsWebMembershipRebootDatabase, SportsWebUser>
    {
        public SportsWebUserRepository(SportsWebMembershipRebootDatabase ctx)
            : base(ctx)
        {
        }
    }
}
