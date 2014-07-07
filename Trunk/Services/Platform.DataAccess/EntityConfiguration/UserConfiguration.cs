using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        #region Construction

        public UserConfiguration()
        {
            Property(u => u.EmailAddress).IsRequired().HasColumnName("email_address").HasMaxLength(100);
            Property(u => u.FirstName).HasColumnName("first_name").HasMaxLength(50);
            Property(u => u.LastName).HasColumnName("last_name").HasMaxLength(50);
            Property(u => u.UserName).HasColumnName("user_name").HasMaxLength(20);
            Property(u => u.Hash).HasColumnName("hash").HasMaxLength(200);
            Property(u => u.Phone).HasColumnName("phone").HasMaxLength(50);
            Property(u => u.SkypeHandle).HasColumnName("skype_handle").HasMaxLength(50);
            Property(u => u.ProviderId).HasColumnName("provider_id").HasMaxLength(30);
            Property(u => u.Provider).HasColumnName("provider").HasMaxLength(30);
            Property(u => u.Locale).HasColumnName("locale").HasMaxLength(30);
            Property(u => u.Gender).HasColumnName("gender").HasMaxLength(10);
            Property(p => p.Id).HasColumnName("user_id");
            Property(p => p.IsAdmin).HasColumnName("is_admin").HasColumnType("bit");

        }

        #endregion
    }

}
