using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceStack.ServiceClient.Web;

namespace SportsWebPt.Platform.Web.Application
{
    public class AjaxHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var isDebug = false;
#if DEBUG
                isDebug = true;
#endif

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                var webServiceException = filterContext.Exception as WebServiceException;
                if (webServiceException != null && webServiceException.StatusCode == 409)
                {
                    HandleAjaxConflictError(filterContext);
                }
                else
                {
                    filterContext.Result = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { message = filterContext.Exception.Message }
                    };
                }
            }

            base.OnException(filterContext);
        }

        private void HandleAjaxConflictError(ExceptionContext filterContext)
        {
            var webServiceException = (WebServiceException)filterContext.Exception;

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { message = webServiceException.ErrorMessage }
            };
        }
    }

}