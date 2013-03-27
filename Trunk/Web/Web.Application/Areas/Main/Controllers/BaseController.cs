using System;
using System.Web;
using System.Web.Mvc;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Logging;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    public abstract class BaseController : Controller
    {
        #region Fields

        protected IUserManagementService _userManagementService;
        protected ILog _logger = LogManager.GetCommonLogger();

        #endregion
        
        #region Construction

        protected BaseController()
        {
            //TODO: this is temp, should not need this service for every controller
            //Check.Argument.IsNotNull(userManagementService, "UserManagementService");
            _userManagementService = DependencyResolver.Current.GetService<IUserManagementService>();
        }

        #endregion

        #region Methods

        public T CreateViewModel<T>(bool excludeUser)
              where T : BaseViewModel, new()
        {
            var viewData = new T
            {
                User = excludeUser ? null : GetUser(),
                GoogleAnalyticsKey = string.Empty
            };

            return viewData;
        }

        public T CreateViewModel<T>()
            where T : BaseViewModel, new()
        {
            return CreateViewModel<T>(false);
        } 

        private User GetUser()
        {
            if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
                return _userManagementService.GetUser(Convert.ToInt32(HttpContext.User.Identity.Name));

            return new User();
        }

        #endregion
    }
}
