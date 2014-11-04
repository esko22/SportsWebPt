using System.Linq;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    public sealed class PlatformDbMigrationsConfiguration : DbMigrationsConfiguration<PlatformDbContext>
    {
        public PlatformDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
        }

        protected override void Seed(PlatformDbContext context)
        {
            //DoSeed(context);
        }

        private void DoSeed(PlatformDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Clinics.AddOrUpdate(p => new { p.Name, p.Website, p.Phone },
                new Clinic { Name = "FunctionSmart", Phone = "(858) 452-0282", Website = "http://www.functionsmart.com/", DisplayImage = "http://www.functionsmart.com/wp-content/uploads/2013/09/fsptlogo.jpg"},
                new Clinic { Name = "SportsWebPt", Phone = "(858) 222-2222", Website = "http://sportswebpt.com/", DisplayImage = "http://sportswebpt.com/Content/images/logoFooterDark.png"});

            context.Therapists.AddOrUpdate(p => p.Id ,  
            new Therapist
            {
                Id = 4,
                Credentials = "SuperCreds",
                Licenses = "Super Licenses"
            }, new Therapist
            {
                Id = 5,
                Credentials = "Not so SuperCreds",
                Licenses = "Not so Super Licenses"
            });

           
            context.ClinicAdminMatrixItems.AddOrUpdate(p => new { p.ClinicId, ClinicAdminId = p.UserId },
                new ClinicAdminMatrixItem { ClinicId = 1, UserId = 4 },
                new ClinicAdminMatrixItem { ClinicId = 2, UserId = 4 },
                new ClinicAdminMatrixItem { ClinicId = 1, UserId = 5 });

            context.ClinicTherapistMatrixItems.AddOrUpdate(p => new { p.ClinicId, p.TherapistId },
                new ClinicTherapistMatrixItem { ClinicId = 1, TherapistId = 4 },
                new ClinicTherapistMatrixItem { ClinicId = 2, TherapistId = 4 },
                new ClinicTherapistMatrixItem { ClinicId = 1, TherapistId = 5 });

            context.ClinicPatientMatrixItems.AddOrUpdate(p => new { p.ClinicId, p.UserId },
                new ClinicPatientMatrixItem { ClinicId = 1, UserId = 4 },
                new ClinicPatientMatrixItem { ClinicId = 2, UserId = 4 },
                new ClinicPatientMatrixItem { ClinicId = 1, UserId = 2 },
                new ClinicPatientMatrixItem { ClinicId = 1, UserId = 3 }
                );

        }
    }
}
