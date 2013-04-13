using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class UserRepo : EFRepository<User>, IUserRepository
    {

        #region Construction

        public UserRepo(DbContext context) : base(context) {}

        #endregion

        #region Methods
        
        public User GetUserByEmailAddress(string emailAddress)
        {
            return DbSet.FirstOrDefault(p => p.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
        }

        public new int Add(User entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
                DbContext.SaveChanges();
            }

            return entity.Id;
        }

        #endregion
    }
}
