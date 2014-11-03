using System.Data.Entity;
using BrockAllen.MembershipReboot;
using SportsWebPt.Identity.Core;

namespace SportsWebPt.Identity.Admin
{
    public class MRConfig
    {
        public static readonly MembershipRebootConfiguration<SportsWebUser> config;
        static MRConfig()
        {
            config = new MembershipRebootConfiguration<SportsWebUser>();
            config.PasswordHashingIterationCount = 10000;
            config.RequireAccountVerification = false;
            //config.EmailIsUsername = true;
        }
    }
}