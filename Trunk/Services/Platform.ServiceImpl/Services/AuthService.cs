using System;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities.Security;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class AuthService : RestService
    {
        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion


      
    }
}
