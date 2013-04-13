﻿using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class OrientationTypeConfiguration : EntityTypeConfiguration<Orientation>
    {
        #region Construction

        public OrientationTypeConfiguration()
        {
            Property(p => p.Id).IsRequired().HasColumnName("orientation_id");
            Property(p => p.Value).IsRequired().HasColumnName("orientation").HasMaxLength(20);
        }

        #endregion
    }
}