using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess.EntityConfiguration;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlatformDbContext : DbContext
    {
        #region Entities

        public DbSet<User> Users { get; set; }
        public DbSet<Orientation> OrientationTypes { get; set; }
        public DbSet<AreaComponent> AreaComponents { get; set; }
        public DbSet<Side> SideTypes { get; set; }
        public DbSet<BodyRegion> RegionTypes { get; set; }
        public DbSet<SkeletonArea> SkeletonAreas { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }

        #endregion

        #region Construction

        public PlatformDbContext()
            : this("sportsWebPtDb")
        {}

        public PlatformDbContext(String connectionStringName)
            :base(connectionStringName)
        {}

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //TODO: reflect over assembly and add dynamically
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new OrientationConfiguration());
            modelBuilder.Configurations.Add(new AreaComponentConfiguration());
            modelBuilder.Configurations.Add(new SideConfiguration());
            modelBuilder.Configurations.Add(new BodyRegionConfiguration());
            modelBuilder.Configurations.Add(new SkeletonAreaConfiguration());
            modelBuilder.Configurations.Add(new SymptomConfiguration());
          
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.ImmediateResults);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.PendingVariants);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.CalculatedResults);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.QueriedVariants);


            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
