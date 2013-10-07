using System.Data.Entity;

using SportsWebPt.Common.DataAccess.Ef;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlatformDbCreateInitializer : 
        DropCreateDatabaseAlways<PlatformDbContext> 
        //DropCreateDatabaseIfModelChanges<PlatformDbContext>
    {
        #region Properties

        public ISeed<PlatformDbContext> Seeder { get; set; }

        #endregion

        #region Methods

        protected override void Seed(PlatformDbContext context)
        {
            Seeder.Seed(context);
        }

        #endregion


    }
}
