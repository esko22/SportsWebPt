using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryPlanMatrixConfiguration : EntityTypeConfiguration<InjuryPlanMatrixItem>
    {

        #region Construction

        public InjuryPlanMatrixConfiguration()
        {
            ToTable("InjuryPlanMatrix");
            Property(p => p.PlanId).IsRequired().HasColumnName("plan_id");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.Id).IsRequired().HasColumnName("injury_plan_matrix_id");
        }

        #endregion

    }
}
