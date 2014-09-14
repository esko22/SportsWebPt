using System;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ClinicConfiguration: EntityTypeConfiguration<Clinic>
    {
        #region Construction

        public ClinicConfiguration()
        {
            ToTable("Clinic");
            Property(p => p.DisplayImage).HasColumnName("display_image").HasMaxLength(1000);
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(200);
            Property(p => p.Phone).IsRequired().HasColumnName("phone").HasMaxLength(20);
            Property(p => p.Id).IsRequired().HasColumnName("clinic_id");
            Property(p => p.Website).HasColumnName("website").HasMaxLength(200);

            HasMany(p => p.Locations).WithRequired().HasForeignKey(f => f.ClinicId);
        }
        
        #endregion
    }

    public class ClinicTherapistMatrixConfiguration : EntityTypeConfiguration<ClinicTherapistMatrixItem>
    {
        #region Construction

        public ClinicTherapistMatrixConfiguration()
        {
            ToTable("ClinicTherapistMatrix");
            Property(p => p.ClinicId).HasColumnName("clinic_id");
            Property(p => p.TherapistId).HasColumnName("therapist_id");
            Property(p => p.UserConfirmed).HasColumnName("user_confirmed");
            Property(p => p.Pin).HasColumnName("pin").HasMaxLength(50);
            Property(p => p.Id).HasColumnName("clinic_therapist_matrix_item_id");
        }

        #endregion
    }

    public class ClinicAdminMatrixConfiguration : EntityTypeConfiguration<ClinicAdminMatrixItem>
    {
        #region Construction

        public ClinicAdminMatrixConfiguration()
        {
            ToTable("ClinicAdminMatrix");
            Property(p => p.ClinicId).HasColumnName("clinic_id");
            Property(p => p.ClinicAdminId).HasColumnName("clinic_admin_id");
            Property(p => p.Id).HasColumnName("clinic_admin_matrix_item_id");
        }

        #endregion
    }

    public class ClinicAdminConfiguration : EntityTypeConfiguration<ClinicAdmin>
    {
        #region Construction

        public ClinicAdminConfiguration()
        {
            ToTable("ClinicAdmin");
            HasKey(k => k.Id);
            Property(p => p.EmergencyContact).HasColumnName("emergency_contact");
            Property(p => p.Id).HasColumnName("clinic_admin_id");

            HasRequired(p => p.User).WithOptional(o => o.ClinicAdmin);
        }

        #endregion
    }

    public class ClinicPatientMatrixConfiguration : EntityTypeConfiguration<ClinicPatientMatrixItem>
    {
        #region Construction

        public ClinicPatientMatrixConfiguration()
        {
            ToTable("ClinicPatientMatrix");
            Property(p => p.ClinicId).HasColumnName("clinic_id");
            Property(p => p.UserId).HasColumnName("user_id");
            Property(p => p.UserConfirmed).HasColumnName("user_confirmed");
            Property(p => p.Pin).HasColumnName("pin").HasMaxLength(50);
            Property(p => p.Id).HasColumnName("clinic_patient_matrix_item_id");
        }

        #endregion
    }


    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        #region Construction

        public LocationConfiguration()
        {
            ToTable("Location");
            Property(p => p.Id).HasColumnName("location_id");
            Property(p => p.Address).HasColumnName("address").HasMaxLength(500);
            Property(p => p.City).HasColumnName("city").HasMaxLength(500);
            Property(p => p.State).HasColumnName("state").HasMaxLength(2);
            Property(p => p.Zipcode).HasColumnName("zipcode");
            Property(p => p.ClinicId).HasColumnName("clinic_id");
        }

        #endregion
    }

    public class ClinicPlanMatrixConfiguration : EntityTypeConfiguration<ClinicPlanMatrixItem>
    {
        #region Construction

        public ClinicPlanMatrixConfiguration()
        {
            ToTable("ClinicPlanMatrix");
            Property(p => p.IsActive).HasColumnName("is_active");
            Property(p => p.PlanId).HasColumnName("plan_id");
            Property(p => p.ClinicId).HasColumnName("clinic_id");
            Property(p => p.Id).HasColumnName("clinic_plan_matrix_item_id");
        }

        #endregion
    }

    public class ClinicExerciseMatrixConfiguration : EntityTypeConfiguration<ClinicExerciseMatrixItem>
    {
        #region Construction

        public ClinicExerciseMatrixConfiguration()
        {
            ToTable("ClinicExerciseMatrix");
            Property(p => p.IsActive).HasColumnName("is_active");
            Property(p => p.ExerciseId).HasColumnName("exercise_id");
            Property(p => p.ClinicId).HasColumnName("clinic_id");
            Property(p => p.Id).HasColumnName("clinic_exercise_matrix_item_id");
        }

        #endregion
    }

    public class ClinicInjurtyMatrixConfiguration : EntityTypeConfiguration<ClinicInjuryMatrixItem>
    {
        #region Construction

        public ClinicInjurtyMatrixConfiguration()
        {
            ToTable("ClinicInjuryMatrix");
            Property(p => p.IsActive).HasColumnName("is_active");
            Property(p => p.InjuryId).HasColumnName("injury_id");
            Property(p => p.ClinicId).HasColumnName("clinic_id");
            Property(p => p.Id).HasColumnName("clinic_injury_matrix_item_id");
        }

        #endregion
    }
}
