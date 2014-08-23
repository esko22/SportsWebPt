using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SessionConfiguration : EntityTypeConfiguration<Session>
    {
          #region Construction

        public SessionConfiguration()
        {
            ToTable("Session");

            Property(p => p.Id).IsRequired().HasColumnName("session_id");
            Property(p => p.Created).IsRequired().HasColumnName("created_on");
            Property(p => p.Executed).HasColumnName("executed_on");
            Property(p => p.ScheduledAt).IsRequired().HasColumnName("scheduled_at");
            Property(p => p.Notes).HasColumnName("notes").HasColumnType("TEXT");
            Property(p => p.EpisodeId).IsRequired().HasColumnName("episode_id");
            Property(p => p.ScheduledWithId).IsRequired().HasColumnName("therapist_id");
            Property(p => p.DifferentialDiagnosisId).IsOptional().HasColumnName("differential_diagnosis_id");
            Property(p => p.SessionType).IsRequired().HasColumnName("session_type_id");
            
            HasOptional(o => o.DifferentialDiagnosis).WithMany().HasForeignKey(k => k.DifferentialDiagnosisId);
            HasRequired(r => r.ScheduledWith).WithMany(m => m.Sessions).HasForeignKey(k => k.ScheduledWithId);
        }            

          #endregion
    }

    public class SessionPlanMatrixConfiguration : EntityTypeConfiguration<SessionPlanMatrixItem>
    {
        #region Construction

        public SessionPlanMatrixConfiguration()
        {
            ToTable("SessionPlanMatrix");

            Property(p => p.Id).IsRequired().HasColumnName("session_plan_matrix_id");
            Property(p => p.SessionId).IsRequired().HasColumnName("session_id");
            Property(p => p.PlanId).IsRequired().HasColumnName("plan_id");
            Property(p => p.Name).IsOptional().HasColumnName("plan_name_override").HasMaxLength(100);

        }

        #endregion
    }
}
