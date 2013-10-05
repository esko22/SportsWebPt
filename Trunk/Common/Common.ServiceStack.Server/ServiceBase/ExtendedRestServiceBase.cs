using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using SportsWebPt.Common.Utilities;

namespace SportsWebPt.Common.ServiceStack
{
    public class ExtendedRestServiceBase<TRequest,TResponse> : RestServiceBase<TRequest>, IDisposable
    {
        public const int DEFAULT_LIMIT = 10;
        public const int MAX_LIMIT = 1024;

        #region Response helpers

        /// <summary>
        /// 200 OK: Successful response
        /// </summary>
        /// <param name="dto">Object to return</param>
        /// <param name="contentType">force a contenttype</param>
        /// <returns></returns>
        public HttpResult Ok(object dto = null, string contentType = null)
        {
            return BuildHttpResult(HttpStatusCode.OK, dto, contentType);
        }

        /// <summary>
        /// 200 OK: Successful response
        /// </summary>
        /// <param name="responseStream">Response stream to return</param>
        /// <param name="contentType">required contenttype</param>
        /// <returns></returns>
        public HttpResult Ok(Stream responseStream, string contentType)
        {
            return BuildHttpResult(HttpStatusCode.OK, responseStream, contentType);
        }


        /// <summary>
        /// 201 Created: A resource was created. Generally the successful response for a POST on a collection
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public HttpResult Created(object dto = null, string contentType = null)
        {
            return BuildHttpResult(HttpStatusCode.Created, dto, contentType);
        }

        /// <summary>
        /// 302 Redirect: A temporary redirect to a given URI (non-cacheable by client). Note that this response may also contain set-cookie headers
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public HttpResult Redirect(string uri)
        {
            return BuildHttpResult(HttpStatusCode.Redirect, headers: new Dictionary<string, string>() { { HttpHeaders.Location, uri } });
        }

        /// <summary>
        /// 400 Bad Request: the request either couldn't be parsed, is missing fields, or has redundant fields. These are generally not business logic exceptions
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public HttpError BadRequest(string message, Exception exception = null)
        {
            return BuildHttpError(HttpStatusCode.BadRequest, message, exception: exception);
        }


        /// <summary>
        /// 401 Unauthorized
        /// </summary>
        /// <returns></returns>
        public HttpError UnAuthorized(string errorCode, string errorMessage, IEnumerable<Notification> notifications = null, Exception exception = null)
        {
            return BuildHttpError(HttpStatusCode.Unauthorized, errorMessage, errorCode, notifications: notifications, exception: exception);
        }

        /// <summary>
        /// 403 Forbidden: User does not have rights to this resource. Generally an authorization error.
        /// </summary>
        /// <returns></returns>
        public HttpError Forbidden(string errorMessage = null, IEnumerable<Notification> notifications = null, Exception exception = null)
        {
            if (errorMessage == null)
            {
                errorMessage = ServiceStackResources.Error403;
            }
            return BuildHttpError(HttpStatusCode.Forbidden, errorMessage, notifications: notifications, exception: exception);
        }

        /// <summary>
        /// 404 Not found: resource does not exist at given path; invalid path
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public HttpError NotFound(string message)
        {
            return BuildHttpError(HttpStatusCode.NotFound, message);
        }

        /// <summary>
        /// 409 Conflict: the request parsed but business logic deems it inappropriate. Generally for POSTs. Examples: invalid state transitions, operation doesn't apply on this instance of the resource (at this time), input contradicts current state
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public HttpError Conflict(string message, IEnumerable<Notification> notifications = null)
        {
            return BuildHttpError(HttpStatusCode.Conflict, message, notifications: notifications);
        }

        /// <summary>
        /// 413 RequestEntityTooLarge: The request is larger than the server is willing or able to process.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public HttpError TooLarge(string message, IEnumerable<Notification> notifications = null)
        {
            return BuildHttpError(HttpStatusCode.RequestEntityTooLarge, message, notifications: notifications);
        }

        /// <summary>
        /// 500 Internal Server Error: only uncaught/unhandled exceptions
        /// </summary>
        /// <returns></returns>
        public HttpError InternalServerError(string errorMessage = null, Exception exception = null)
        {
            return BuildHttpError(HttpStatusCode.InternalServerError, errorMessage ?? ServiceStackResources.Error500, exception: exception);
        }


        private HttpError BuildHttpError(System.Net.HttpStatusCode statusCode, string message, string errorCode = null, string contentType = null, Exception exception = null, IEnumerable<Notification> notifications = null)
        {
            // Remove ServiceStack's html page from getting displayed in browsers
            if (contentType == null)
            {
                contentType = ContentType.Json;
            }

            if (errorCode == null)
                errorCode = statusCode.ToString();

            if (notifications == null)
                notifications = new List<Notification> { new Notification() { Item = message, Type = "error" } };

            HttpError error;
            if (exception == null)
            {
                error = new HttpError(statusCode, errorCode, (string)null);
            }
            else
            {
                error = new HttpError(statusCode, exception);
            }

            error.StatusDescription = statusCode.ToString().SplitWordByCapitalLetters(" ");
            error.ContentType = contentType;
            var errorResponse = new ErrorResponse(message, errorCode);
            if (notifications != null)
            {
                errorResponse.Notifications.AddRange(notifications);
            }
            error.Response = errorResponse;
            return error;
        }

        private HttpResult BuildHttpResult(HttpStatusCode statusCode, Stream responseStream, string contentType, Dictionary<string, string> headers = null)
        {
            var ret = new HttpResult(responseStream, contentType);
            ret.StatusCode = statusCode;
            ret.StatusDescription = statusCode.ToString().SplitWordByCapitalLetters(" ");
            if (headers != null)
            {
                headers.ForEach(x => ret.Headers[x.Key] = x.Value);
            }
            return ret;
        }

        private HttpResult BuildHttpResult(HttpStatusCode statusCode, object dto = null, string contentType = null, Dictionary<string, string> headers = null)
        {
            // DEQ20121018: disabling this, as it seems to totally clobber ServiceStack's built-in format handling...
            // if the html page is not desired, proper way to disable is: ContentTypeFilters.ClearCustomFilters() in Configure()
            //
            // Remove ServiceStack's html page from getting displayed in browsers
            //if (contentType == null)
            //{
            //    contentType = ContentType.Json;
            //}

            var ret = new HttpResult
                          {
                              StatusCode = statusCode,
                              StatusDescription = statusCode.ToString().SplitWordByCapitalLetters(" "),
                              Response = dto,
                              //ContentType = contentType,
                              
                          };

            if (headers != null)
            {
                headers.ForEach(x => ret.Headers[x.Key] = x.Value);
            }
            return ret;
        }


        #endregion

        public virtual void Dispose() { }
    }
}
