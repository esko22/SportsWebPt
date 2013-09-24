using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IUserManagementService : IDisposable
    {
        User GetUser(String emailAddress);

        User GetUser(int id);

        int AddUser(User user);

        User Auth(String emailAddress, String hash);

        void AddFavorite(UserFavorite userFavorite);
    }
}
