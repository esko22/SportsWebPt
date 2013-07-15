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
        public DbSet<Orientation> Orientations { get; set; }
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<BodyRegion> BodyRegions { get; set; }
        public DbSet<SkeletonArea> SkeletonAreas { get; set; }
        public DbSet<BodyPartMatrixItem> BodyPartMatrix { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<SymptomMatrixItem> SymptomMatrix { get; set; }
        public DbSet<DifferentialDiagnosis> DifferentialDiagnoses { get; set; }
        public DbSet<SymptomDetail> SymptomDiagnosisResults { get; set; }
        public DbSet<Injury> Injuries { get; set; }
        public DbSet<InjurySymptomMatrixItem> InjurySymptomMatrix { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseEquipmentMatrixItem> ExerciseEquipmentMatrixItems { get; set; }
        public DbSet<ExerciseVideoMatrixItem> ExerciseVideoMatrixItems { get; set; }
        public DbSet<InjuryCauseMatrixItem> InjuryCauseMatrixItems { get; set; }
        public DbSet<InjurySignMatrixItem> InjurySignMatrixItems { get; set; }
        public DbSet<InjuryWorkoutMatrixItem> InjuryWorkoutMatrixItems { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExerciseMatrixItem> WorkoutExceriseMatrixItems { get; set; }
        public DbSet<ExerciseBodyRegionMatrixItem> ExerciseBodyPartMatrixItems { get; set; }  

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
            modelBuilder.Configurations.Add(new BodyPartConfiguration());
            modelBuilder.Configurations.Add(new SideConfiguration());
            modelBuilder.Configurations.Add(new BodyRegionConfiguration());
            modelBuilder.Configurations.Add(new SkeletonAreaConfiguration());
            modelBuilder.Configurations.Add(new BodyPartMatrixConfiguration());
            modelBuilder.Configurations.Add(new SymptomConfiguration());
            modelBuilder.Configurations.Add(new SymptomMatrixConfiguration());
            modelBuilder.Configurations.Add(new DifferentialDiagnosisConfiguration());
            modelBuilder.Configurations.Add(new SymptomDetailConfiguration());
            modelBuilder.Configurations.Add(new InjuryConfiguration());
            modelBuilder.Configurations.Add(new InjurySymptomMatrixConfiguration());
            modelBuilder.Configurations.Add(new CauseConfiguration());
            modelBuilder.Configurations.Add(new EquipmentConfiguration());
            modelBuilder.Configurations.Add(new ExerciseConfiguration());
            modelBuilder.Configurations.Add(new ExerciseEquipmentMatrixConfiguration());
            modelBuilder.Configurations.Add(new ExerciseVideoMatrixConfiguration());
            modelBuilder.Configurations.Add(new InjuryCauseMatrixConfiguration());
            modelBuilder.Configurations.Add(new InjurySignMatrixConfiguration());
            modelBuilder.Configurations.Add(new InjuryWorkoutMatrixConfiguration());
            modelBuilder.Configurations.Add(new SignConfiguration());
            modelBuilder.Configurations.Add(new VideoConfiguration());
            modelBuilder.Configurations.Add(new WorkoutConfiguration());
            modelBuilder.Configurations.Add(new WorkoutExerciseMatrixConfiguration());
            modelBuilder.Configurations.Add(new ExerciseBodyRegionMatrixConfiguration());

            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.ImmediateResults);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.PendingVariants);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.CalculatedResults);
            //modelBuilder.Entity<AnnotationJob>().Ignore(p => p.QueriedVariants);


            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
