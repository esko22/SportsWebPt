using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlanCategoryMatrixConfiguration : EntityTypeConfiguration<PlanCategoryMatrixItem>
    {
        #region Construction

        public PlanCategoryMatrixConfiguration()
        {
            ToTable("PlanCategoryMatrix");
            HasKey(p => new {p.PlanId, p.Category});
            Property(p => p.Category).IsRequired().HasColumnName("function_category_id");
            Property(p => p.PlanId).IsRequired().HasColumnName("plan_id");
        }

        #endregion
    }
}
