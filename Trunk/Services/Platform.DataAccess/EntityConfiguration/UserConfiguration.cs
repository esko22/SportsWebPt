using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        #region Construction

        public UserConfiguration()
        {
            Property(u => u.ExternalAccountId).HasColumnName("external_account_id").HasMaxLength(200);
            Property(p => p.Id).HasColumnName("user_id");
            Property(p => p.AccountLinked).HasColumnName("account_linked").HasColumnType("bit");

        }

        #endregion
    }

}
