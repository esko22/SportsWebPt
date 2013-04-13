using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        #region Construction

        public UserConfiguration()
        {
            Property(u => u.EmailAddress).IsRequired();
            //Property(u => u.UserName).IsRequired();
        }

        #endregion
    }
}
