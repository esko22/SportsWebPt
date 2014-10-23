using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        #region Construction

        public UserConfiguration()
        {
            Property(u => u.EmailAddress).HasColumnName("email_address").HasMaxLength(100);
            Property(u => u.Hash).HasColumnName("hash").HasMaxLength(200);
            Property(p => p.Id).HasColumnName("user_id");
            Property(p => p.AccountLinked).HasColumnName("account_linked").HasColumnType("bit");

        }

        #endregion
    }

}
