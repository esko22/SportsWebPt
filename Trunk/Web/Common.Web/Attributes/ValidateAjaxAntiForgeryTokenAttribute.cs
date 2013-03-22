using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SportsWebPt.Common.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateAjaxAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
 
            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];

            AntiForgery.Validate(cookie != null ? cookie.Value : null, 
                                 httpContext.Request.Headers["__RequestVerificationToken"]);
        }
    }
}
