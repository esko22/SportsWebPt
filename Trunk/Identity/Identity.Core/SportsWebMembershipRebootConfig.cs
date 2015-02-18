using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot;

namespace SportsWebPt.Identity.Core
{
    public class SportsWebMembershipRebootConfig : MembershipRebootConfiguration<SportsWebUser>
    {
        public static readonly SportsWebMembershipRebootConfig Config;

        static SportsWebMembershipRebootConfig()
        {
            Config = new SportsWebMembershipRebootConfig
            {
                PasswordHashingIterationCount = 10000,
                RequireAccountVerification = false
            };
            //config.EmailIsUsername = true;
        }
    }
}
