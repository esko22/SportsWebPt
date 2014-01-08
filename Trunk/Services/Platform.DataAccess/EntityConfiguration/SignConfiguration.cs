using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SignConfiguration : EntityTypeConfiguration<Sign>
    {
        #region Construction

        public SignConfiguration()
        {
            ToTable("Sign");
            Property(p => p.Category).IsRequired().HasColumnName("category").IsRequired();
            Property(p => p.Description).HasColumnName("description").IsRequired().HasMaxLength(200);
            Property(p => p.Id).IsRequired().HasColumnName("sign_id");
            Property(p => p.FilterCategoryId).IsRequired().HasColumnName("sign_filter_id");
        }

        #endregion
    }
}
