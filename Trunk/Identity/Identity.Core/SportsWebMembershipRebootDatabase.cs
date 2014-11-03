using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;

namespace SportsWebPt.Identity.Core
{
    public class SportsWebMembershipRebootDatabase : MembershipRebootDbContext<SportsWebUser>
    {

        public SportsWebMembershipRebootDatabase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {}

        public SportsWebMembershipRebootDatabase()
            :base()
        {}

    }
}
