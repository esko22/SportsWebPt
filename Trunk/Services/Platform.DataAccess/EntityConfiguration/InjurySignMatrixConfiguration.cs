using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class InjurySignMatrixConfiguration : EntityTypeConfiguration<InjurySignMatrixItem>
    {
        #region Construction

        public InjurySignMatrixConfiguration()
        {
            ToTable("InjurySignMatrix");
            Property(p => p.SignId).IsRequired().HasColumnName("sign_id");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.Id).IsRequired().HasColumnName("injury_sign_matrix_id");
        }

        #endregion
    }
}
