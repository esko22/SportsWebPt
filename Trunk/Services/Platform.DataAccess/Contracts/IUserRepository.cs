using System;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IUserRepository : IRepository<User>
    {
        #region Methods

        User GetUserByEmailAddress(String emailAddress);

        #endregion
    }
}
