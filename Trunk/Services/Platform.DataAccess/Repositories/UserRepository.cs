using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion
    }
}
