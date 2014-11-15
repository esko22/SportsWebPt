using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class EpisodeConfiguration : EntityTypeConfiguration<Episode>
    {
          #region Construction

        public EpisodeConfiguration()
        {
            ToTable("Episode");

            Property(p => p.Id).IsRequired().HasColumnName("episode_id");
            Property(p => p.CreatedOn).IsRequired().HasColumnName("created_on");
            Property(p => p.PrognosisId).IsOptional().HasColumnName("initial_prognosis_id");
            Property(p => p.State).IsRequired().HasColumnName("episode_state_id");
            Property(p => p.ClinicPatientId).IsRequired().HasColumnName("clinic_patient_id");
            Property(p => p.TherapistId).IsRequired().HasColumnName("therapist_id");
            Property(p => p.ClinicId).IsRequired().HasColumnName("clinic_id");
            Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(p => p.Description).IsOptional().HasColumnType("TEXT").HasColumnName("description");

            HasMany(m => m.Sessions).WithRequired(r => r.Episode);
            HasRequired(r => r.ClinicPatient).WithMany(m => m.Episodes).HasForeignKey(k => k.ClinicPatientId);
            HasRequired(r => r.Therapist).WithMany(m => m.Episodes).HasForeignKey(k => k.TherapistId);
            HasOptional(o => o.Prognosis).WithMany(m => m.Episodes).HasForeignKey(k => k.PrognosisId);
        }            

          #endregion
    }
}
