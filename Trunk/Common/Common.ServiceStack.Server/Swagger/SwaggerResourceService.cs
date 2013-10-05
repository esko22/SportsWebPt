using System;

using SportsWebPt.Common.Utilities.ServiceApi;

namespace SportsWebPt.Common.ServiceStack
{
    public class SwaggerResourceService : LoggingRestServiceBase<SwaggerResourceRequest,object>
    {
        #region Properties

        public IBaseApiConfig ApiConfig { get; set; }

        public ApiDocumentGenerator DocumentGenerator { get; set; }

        #endregion

        #region Methods

        public override object OnGet(SwaggerResourceRequest request)
        {
            //TODO: remove after testing
            DocumentGenerator.Generate();

            return Ok(new
                       {
                           apiVersion = ApiConfig.ApiVersion,
                           basePath = ApiConfig.ApiUriWithVersion,
                           apis = DocumentGenerator.ApiResourceListing.Values
                       });
        }

        #endregion
    }
}
