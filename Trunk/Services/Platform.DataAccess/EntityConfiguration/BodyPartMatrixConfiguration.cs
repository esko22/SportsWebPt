using System;
using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class BodyPartMatrixConfiguration : EntityTypeConfiguration<BodyPartMatrixItem>
    {
        #region Construction

        public BodyPartMatrixConfiguration()
        {
            ToTable("BodyPartMatrix");
            Property(p => p.SkeletonAreaId).HasColumnName("skeleton_area_id");
            Property(p => p.BodyPartId).HasColumnName("body_part_id");
            Property(p => p.IsSecondary).HasColumnName("is_secondary");
            Property(p => p.Id).HasColumnName("body_part_matrix_id");
            Property(p => p.Decom).HasColumnName("decom");
        }

        #endregion
    }
}
