using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        #region Construction

        public UserEntityConfiguration()
        {
            Property(u => u.EmailAddress).IsRequired();
            //Property(u => u.UserName).IsRequired();
        }

        #endregion
    }
}
