using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;

namespace SportsWebPt.Identity.Admin
{
    public class CustomDatabase : MembershipRebootDbContext<CustomUser, CustomGroup>
    {
        static CustomDatabase()
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<DefaultMembershipRebootDatabase, BrockAllen.MembershipReboot.Ef.Migrations.Configuration>());
        }

        public CustomDatabase()
            : this("CustomMembershipReboot")
        {
        }

        public CustomDatabase(string name)
            :base(name)
        {
        }
    }
}