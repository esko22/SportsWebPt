﻿using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseConfiguration : EntityTypeConfiguration<Exercise>
    {

        #region Construction

        public ExerciseConfiguration()
        {
            ToTable("Exercise");
            Property(p => p.Description).HasColumnName("description").HasMaxLength(500);
            Property(p => p.Difficulty).HasColumnName("difficulty").IsRequired();
            Property(p => p.Duration).HasColumnName("duration").IsRequired();
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
            Property(p => p.PageName).IsRequired().HasColumnName("page_name").HasMaxLength(50);
            Property(p => p.Tags).HasColumnName("tags").HasMaxLength(1000);
            Property(p => p.Id).IsRequired().HasColumnName("exercise_id");
        }

        #endregion

    }
}
