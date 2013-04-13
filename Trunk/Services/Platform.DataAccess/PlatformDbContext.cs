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
        public DbSet<OrientationType> OrientationTypes { get; set; }
        public DbSet<PartType> PartTypes { get; set; }
        public DbSet<SideType> SideTypes { get; set; }
        public DbSet<RegionType> RegionTypes { get; set; }
        public DbSet<SkeletonHotspot> SkeletonHotspots { get; set; } 

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
            modelBuilder.Configurations.Add(new OrientationTypeConfiguration());
            modelBuilder.Configurations.Add(new PartTypeConfiguration());
            modelBuilder.Configurations.Add(new SideTypeConfiguration());
            modelBuilder.Configurations.Add(new RegionTypeConfiguration());
            modelBuilder.Configurations.Add(new SkeletorHotspotConfiguration());


            //modelBuilder.Entity<AnnotationCollection>()
            //    .HasMany(p => p.AnnotationSources)
            //    .WithMany(t => t.AnnotationCollections)
            //    .Map(mc =>
            //    {
            //        mc.ToTable("CollectionSources");
            //        mc.MapLeftKey("CollectionId");
            //        mc.MapRightKey("SourceId");
            //    });

            //modelBuilder.Entity<AnnotationCollection>()
            //    .HasMany(p => p.ReferenceGenomes)
            //    .WithMany(t => t.AnnotationCollections)
            //    .Map(mc =>
            //    {
            //        mc.ToTable("GenomeCollections");
            //        mc.MapLeftKey("CollectionId");
            //        mc.MapRightKey("ReferenceGenomeId");
            //    });

            //modelBuilder.Entity<AnnotationCollection>().HasRequired(p => p.AnnotatedGenome);
            //modelBuilder.Entity<ReferenceGenome>().HasRequired(p => p.Provider);
            //modelBuilder.Entity<ReferenceGenome>().HasRequired(p => p.Species);

            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.ImmediateResults);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.PendingVariants);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.CalculatedResults);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.QueriedVariants);


            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
