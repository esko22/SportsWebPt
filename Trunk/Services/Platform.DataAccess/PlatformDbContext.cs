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
        public DbSet<InjuryPlanMatrixItem> InjuryPlanMatrixItems { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanExerciseMatrixItem> PlansExceriseMatrixItems { get; set; }
        public DbSet<ExerciseBodyRegionMatrixItem> ExerciseBodyRegionMatrixItems { get; set; }
        public DbSet<PlanBodyRegionMatrixItem> PlanBodyRegionMatrixItems { get; set; }
        public DbSet<InjuryBodyRegionMatrixItem> InjuryBodyRegionMatrixItems { get; set; }
        public DbSet<SymptomRenderType> SymptomRenderTypes { get; set; }
        public DbSet<ExerciseBodyPartMatrixItem> ExerciseBodyPartMatrixItems { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<ExerciseCategoryMatrixItem> ExerciseCategoryMatrixItems { get; set; }
        public DbSet<PlanCategoryMatrixItem> PlanCategoryMatrixItems { get; set; }
        public DbSet<VideoCategoryMatrixItem> VideoCategoryMatrixItems { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<InjuryTreatmentMatrixItem> InjuryTreatmentMatrixItems { get; set; }
        public DbSet<Prognosis> Prognoses { get; set; }
        public DbSet<InjuryPrognosisMatrixItem> InjuryPrognosisMatrixItems { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<ClinicTherapistMatrixItem> ClinicTherapistMatrixItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ClinicAdminMatrixItem> ClinicAdminMatrixItems { get; set; }
        public DbSet<ClinicPatientMatrixItem> ClinicPatientMatrixItems { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<ClinicExerciseMatrixItem> ClinicExerciseMatrixItems { get; set; }
        public DbSet<ClinicPlanMatrixItem> ClinicPlanMatrixItems { get; set; }
        public DbSet<ClinicInjuryMatrixItem> ClinicInjuryMatrixItems { get; set; }
        public DbSet<TherapistExerciseMatrixItem> TherapistExerciseMatrixItems { get; set; }
        public DbSet<TherapistPlanMatrixItem> TherapistPlanMatrixItems { get; set; }
        public DbSet<ExercisePublishDetail> ExercisePublishDetails { get; set; }
        public DbSet<PlanPublishDetail> PlanPublishDetails { get; set; }
        public DbSet<InjuryPublishDetail> InjuryPublishDetails { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionPlanMatrixItem> SessionPlanMatrixItems { get; set; } 

        #endregion

        #region Construction

        public PlatformDbContext()
            : this("sportsWebPtDb")
        {}

        public PlatformDbContext(String connectionStringName)
            : base(connectionStringName)
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
            modelBuilder.Configurations.Add(new InjuryPlanMatrixConfiguration());
            modelBuilder.Configurations.Add(new SignConfiguration());
            modelBuilder.Configurations.Add(new VideoConfiguration());
            modelBuilder.Configurations.Add(new PlanConfiguration());
            modelBuilder.Configurations.Add(new PlanExerciseMatrixConfiguration());
            modelBuilder.Configurations.Add(new ExerciseBodyRegionMatrixConfiguration());
            modelBuilder.Configurations.Add(new InjuryBodyRegionMatrixConfiguration());
            modelBuilder.Configurations.Add(new PlanBodyRegionMatrixConfiguration());
            modelBuilder.Configurations.Add(new SymptomRenderTypeConfiguration());
            modelBuilder.Configurations.Add(new ExerciseBodyPartMatrixConfiguration());
            modelBuilder.Configurations.Add(new VendorConfiguration());
            modelBuilder.Configurations.Add(new ExerciseCategoryMatrixConfiguration());
            modelBuilder.Configurations.Add(new PlanCategoryMatrixConfiguration());
            modelBuilder.Configurations.Add(new VideoCategoryMatrixConfiguration());
            modelBuilder.Configurations.Add(new FilterConfiguration());
            modelBuilder.Configurations.Add(new TreatmentConfiguration());
            modelBuilder.Configurations.Add(new InjuryTreatmentMatrixConfiguration());
            modelBuilder.Configurations.Add(new ClinicConfiguration());
            modelBuilder.Configurations.Add(new ClinicTherapistMatrixConfiguration());
            modelBuilder.Configurations.Add(new ClinicPatientMatrixConfiguration());
            modelBuilder.Configurations.Add(new ClinicAdminMatrixConfiguration());
            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new TherapistConfiguration());
            modelBuilder.Configurations.Add(new ClinicExerciseMatrixConfiguration());
            modelBuilder.Configurations.Add(new ClinicInjurtyMatrixConfiguration());
            modelBuilder.Configurations.Add(new ClinicPlanMatrixConfiguration());
            modelBuilder.Configurations.Add(new TherapistExerciseMatrixConfiguration());
            modelBuilder.Configurations.Add(new TherapistPlanMatrixConfiguration());
            modelBuilder.Configurations.Add(new ExercisePublishDetailConfiguration());
            modelBuilder.Configurations.Add(new InjuryPublishDetailConfiguration());
            modelBuilder.Configurations.Add(new PlanPublishDetailConfiguration());
            modelBuilder.Configurations.Add(new EpisodeConfiguration()); 
            modelBuilder.Configurations.Add(new SessionConfiguration()); 
            modelBuilder.Configurations.Add(new SessionPlanMatrixConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
