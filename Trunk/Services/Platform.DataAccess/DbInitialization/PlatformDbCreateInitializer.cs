using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess.Ef;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlatformDbCreateInitializer : CreateDatabaseIfNotExists<PlatformDbContext>
    {
        #region Properties

        public ISeed<PlatformDbContext> Seeder { get; set; }

        #endregion


        #region Construction

        public PlatformDbCreateInitializer()
        {
            //TODO: look at making this swappable
            Seeder = new PlatformDbDefaultSeeder();
        }

        #endregion

        #region Methods

        protected override void Seed(PlatformDbContext context)
        {
            Seeder.Seed(context);
        }

        #endregion


    }
}
