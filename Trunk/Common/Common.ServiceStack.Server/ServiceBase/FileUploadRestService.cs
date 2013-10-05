using System;
using System.IO;
using System.Linq;

using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SportsWebPt.Common.ServiceStack
{
    public abstract class FileUploadRestService : RestService
    {
        #region Fields

        private readonly string[] _illegalFileExtensions = new[] { ".exe", ".bat", ".com", ".cmd", ".reg", ".vb", ".vbs" };

        #endregion

        #region Methods

        protected IHttpResult ValidateUploadRequest(AbstractFileUploadRequest request,
                out string fileName, out string directory, out string contentType, out long contentLength, out Stream contentStream)
        {

            var file = RequestContext.Files.FirstOrDefault();
            var httpRequest = RequestContext.Get<IHttpRequest>();
            var multiPart = request.MultiPart;
            directory = request.Directory;

            if (file != null)
            {
                // Via multipart/form-data
                fileName = file.FileName;
                contentLength = file.ContentLength;
                contentType = file.ContentType;
                contentStream = file.InputStream;
            }
            else // Content as request body
            {
                fileName = request.Name;
                contentLength = httpRequest.ContentLength;
                contentType = httpRequest.ContentType;
                contentStream = httpRequest.InputStream;
            }

            if(contentLength == 0)
                return BadRequest("File content not provided");

            // this is to support the ServiceStack/.NET SDK that sends requests in that way
            if (contentType == null && httpRequest != null && httpRequest.ContentType != null)
                contentType = httpRequest.ContentType;


            if (string.IsNullOrEmpty(contentType))
                return BadRequest("Content-Type not provided");

            if (directory != null)
            {
                if (directory == string.Empty || directory == "/")
                    directory = null;
                else
                {
                    directory = directory.Trim('/');
                    if (Path.GetInvalidPathChars().Union(new[] { '\\' }).Intersect(directory).Any())
                        return BadRequest("The directory has an invalid character");
                }
            }

            if (string.IsNullOrEmpty(fileName))
                return BadRequest("The file name was not provided");

            if (fileName.Length > 64)
                return BadRequest("The file name is too long");

            if (Path.GetInvalidFileNameChars().Intersect(fileName).Any())
                return BadRequest("The file name has an invalid character");

            switch (contentType)
            {
                // List of unsupported content-types
                case ContentType.FormUrlEncoded:
                    return BadRequest(string.Format("Content-Type of '{0}' is not supported", contentType));
            }

            var fileInvalidResponse = ValidateFileExtensions(Path.GetExtension(fileName));
            if (fileInvalidResponse != null)
                return fileInvalidResponse;

            var contentmd5 = httpRequest.Headers["Content-MD5"];

            if (contentmd5 != null && multiPart)
                return BadRequest("The header 'Content-MD5' is only supported for individual parts for multipart uploads");

            return null;
        }

        protected virtual IHttpResult ValidateFileExtensions(string fileExtension)
        {
            if (String.IsNullOrEmpty(fileExtension) || _illegalFileExtensions.Contains(fileExtension))
                return BadRequest(string.Format("File extension '{0}' is not supported", fileExtension));

            return null;
        } 

        #endregion

    }
}
