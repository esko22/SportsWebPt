using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjuryCauseMatrixConfiguration : EntityTypeConfiguration<InjuryCauseMatrixItem>
    {
        #region Construction

        public InjuryCauseMatrixConfiguration()
        {
            ToTable("InjuryCauseMatrix");
            Property(p => p.CauseId).IsRequired().HasColumnName("cause_id");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.Id).IsRequired().HasColumnName("injury_cause_matrix_id");

        }

        #endregion
    }
}
