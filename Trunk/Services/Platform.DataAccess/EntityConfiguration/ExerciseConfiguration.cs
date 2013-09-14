using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseConfiguration : EntityTypeConfiguration<Exercise>
    {

        #region Construction

        public ExerciseConfiguration()
        {
            ToTable("Exercise");
            Property(p => p.Description).HasColumnName("description").HasColumnType("TEXT");
            Property(p => p.Difficulty).HasColumnName("difficulty").IsRequired();
            Property(p => p.Duration).HasColumnName("duration").IsRequired();
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
            Property(p => p.MedicalName).IsOptional().HasColumnName("medical_name").HasMaxLength(100);
            Property(p => p.PageName).IsRequired().HasColumnName("page_name").HasMaxLength(50);
            Property(p => p.Tags).HasColumnName("tags").HasColumnType("TEXT").IsOptional();
            Property(p => p.Sets).HasColumnName("sets");
            Property(p => p.Repititions).HasColumnName("repititions");
            Property(p => p.PerDay).HasColumnName("per_day");
            Property(p => p.PerWeek).HasColumnName("per_week");
            Property(p => p.HoldFor).HasColumnName("hold_for");
            Property(p => p.Id).IsRequired().HasColumnName("exercise_id");
            Property(p => p.HoldType).HasColumnName("hold_type_id").IsRequired();

            HasMany(m => m.ExerciseCategoryMatrixItems).WithRequired(r => r.Exercise).HasForeignKey(fk => fk.ExerciseId);

        }

        #endregion

    }
}
