﻿using System;
using System.Data.Entity.ModelConfiguration;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjurySymptomMatrixItemConfiguration : EntityTypeConfiguration<InjurySymptomMatrixItem>
    {
        #region Construction

        public InjurySymptomMatrixItemConfiguration()
        {
            ToTable("InjurySymptomMatrixItem");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.SymptomMatrixItemId).HasColumnName("symptom_matrix_item_id");
            Property(p => p.ThresholdValue).IsRequired().HasColumnName("threshold_value");
            Property(p => p.Id).IsRequired().HasColumnName("injury_symptom_matrix_item_id");
        }
        
        #endregion
    }
}