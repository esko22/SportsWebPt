using System;
using System.Collections.Generic;
using System.IO;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace SportsWebPt.Common.ServiceStack
{
    public class RestService : Service
    {

        #region Response helpers

        public HttpResult Ok(object dto = null, string contentType = null)
        {
            return HttpResponseFormatter.Ok(dto, contentType);
        }

        public HttpResult Ok(Stream responseStream, string contentType)
        {
            return HttpResponseFormatter.Ok(responseStream, contentType);
        }

        public HttpResult Created(object dto = null, string contentType = null)
        {
            return HttpResponseFormatter.Created(dto, contentType);
        }

        public HttpResult Redirect(string uri)
        {
            return HttpResponseFormatter.Redirect(uri);
        }

        public HttpError BadRequest(string message, Exception exception = null)
        {
            return HttpResponseFormatter.BadRequest(message, exception: exception);
        }

        public HttpError UnAuthorized(string errorCode, string errorMessage, IEnumerable<Notification> notifications = null, Exception exception = null)
        {
            return HttpResponseFormatter.UnAuthorized(errorMessage, errorCode, notifications: notifications, exception: exception);
        }

        public HttpError Forbidden(string errorMessage = null, IEnumerable<Notification> notifications = null, Exception exception = null)
        {
            return HttpResponseFormatter.Forbidden(errorMessage, notifications: notifications, exception: exception);
        }

        public HttpError NotFound(string message)
        {
            return HttpResponseFormatter.NotFound(message);
        }

        public HttpError Conflict(string message, IEnumerable<Notification> notifications = null)
        {
            return HttpResponseFormatter.Conflict(message, notifications: notifications);
        }

        public HttpError TooLarge(string message, IEnumerable<Notification> notifications = null)
        {
            return HttpResponseFormatter.TooLarge(message, notifications: notifications);
        }

        public HttpError InternalServerError(string errorMessage = null, Exception exception = null)
        {
            return HttpResponseFormatter.InternalServerError(errorMessage ?? ServiceStackResources.Error500, exception: exception);
        }

        #endregion
    }
}
