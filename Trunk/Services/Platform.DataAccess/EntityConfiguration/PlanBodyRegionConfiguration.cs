using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlanBodyRegionMatrixConfiguration : EntityTypeConfiguration<PlanBodyRegionMatrixItem>
    {

        #region Construction

        public PlanBodyRegionMatrixConfiguration()
        {
            ToTable("PlanBodyRegionMatrix");
            Property(p => p.BodyRegionId).IsRequired().HasColumnName("body_region_id");
            Property(p => p.PlanId).IsRequired().HasColumnName("plan_id");
            Property(p => p.Id).IsRequired().HasColumnName("plan_body_region_matrix_id");
        }

        #endregion

    }
}
