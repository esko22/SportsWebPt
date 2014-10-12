using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Relational;

namespace SportsWebPt.Identity.Admin
{
    public class MRConfig
    {
        public static readonly MembershipRebootConfiguration<RelationalUserAccount> config;
        static MRConfig()
        {
            config = new MembershipRebootConfiguration<RelationalUserAccount>();
            config.PasswordHashingIterationCount = 10000;
            config.RequireAccountVerification = false;
            //config.EmailIsUsername = true;
        }
    }
}