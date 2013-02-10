using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.UOW
{
    public class UserUOW : IDisposable
    {
        #region Fields

        private PlatformDbContext _context;

        #endregion

        #region Construction

        public UserUOW()
        {
            _context = new PlatformDbContext();
        }
        
        #endregion

        #region Methods

        public User GetUser(int id)
        {
            return _context.Users.SingleOrDefault(p => p.Id == id);
        }

        #endregion

        public void Dispose()
        {
            if(_context != null)
                _context.Dispose();
        }
    }
}
