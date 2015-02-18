using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;

namespace SportsWebPt.Identity.Core
{

    public class SportsWebGroup : RelationalGroup
    {
        public virtual string Description { get; set; }
    }

    public class SportsWebGroupService : GroupService<SportsWebGroup>
    {
        public SportsWebGroupService(SportsWebGroupRepository repo, SportsWebMembershipRebootConfig config)
            : base(config.DefaultTenant, repo)
        {

        }
    }

    public class SportsWebGroupRepository : DbContextGroupRepository<SportsWebMembershipRebootDatabase, SportsWebGroup>
    {
        public SportsWebGroupRepository(SportsWebMembershipRebootDatabase ctx)
            : base(ctx)
        {
        }
    }
}
