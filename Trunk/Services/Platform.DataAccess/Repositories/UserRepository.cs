using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {

        #region Construction

        public UserRepository(DbContext context) : base(context) {}

        #endregion

        #region Methods
        
        public User GetUserByEmailAddress(string emailAddress)
        {
            throw new NotImplementedException();
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
