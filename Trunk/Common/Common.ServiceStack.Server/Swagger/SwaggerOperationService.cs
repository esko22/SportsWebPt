using System.Collections.Generic;
using System.Linq;
using SportsWebPt.Common.Utilities.ServiceApi;

namespace SportsWebPt.Common.ServiceStack
{
    public class SwaggerOperationService : LoggingRestServiceBase<SwaggerOperationRequest, object>
    {
        #region Properties

        public ApiDocumentGenerator DocumentGenerator { get; set; }

        public IBaseApiConfig ApiConfig { get; set; }

        #endregion

        #region Methods

        public override object OnGet(SwaggerOperationRequest request)
        {
            //TODO: remove after testing
            //DocumentGenerator.Generate();

            if (!DocumentGenerator.ApiResourceListing.ContainsKey(request.resource) ||
                !DocumentGenerator.ApiOperationListing.ContainsKey(request.resource))
                return BadRequest("Invalid resource request, resource does not exist");

            var apiResourceInfo = DocumentGenerator.ApiResourceListing[request.resource];
            var apiOperationList = DocumentGenerator.ApiOperationListing[request.resource];
            var apiModelList = DocumentGenerator.ApiModelListing[request.resource];

            var resourceOperations = new
                                         {
                                             apiVersion = ApiConfig.ApiVersion,
                                             basePath = ApiConfig.ApiUriWithVersion,
                                             resourcePath = apiResourceInfo["path"],
                                             apis = apiOperationList.Values.ToArray(),
                                             models = apiModelList.Values.ToArray()
                                         };

            return Ok(resourceOperations);
        }

        private List<Dictionary<string, object>> GenDummyOp()
        {
            var listofApis = new List<Dictionary<string, object>>();
            var operation = new Dictionary<string, object>()
                                {
                                        {"httpMethod", "GET"},
                                        {"nickname", "Micky Mouse"},
                                        {"responseClass", "Micky Mouse"},
                                        //{"parameters", new List<Dictionary<String,object>>()},
                                        {"summary", "Micky Mouse"}
                                    };

            var operations = new List<Dictionary<string,object>>();
            operations.Add(operation);

            var operation2 = new Dictionary<string, object>()
                                {
                                        {"httpMethod", "POST"},
                                        {"nickname", "Micky Mouse"},
                                        {"responseClass", "Micky Mouse"},
                                        //{"parameters", new List<Dictionary<String,object>>()},
                                        {"summary", "Micky Mouse"}
                                    };

            
            
            operations.Add(operation2);

            var apiEndpoint = new Dictionary<string, object>()
                                  {
                                      {"path", "/collection/{Id}/variants"},
                                      {"description", "test this BS"},
                                      {"operations", operations }
                                  };
            listofApis.Add(apiEndpoint);
            listofApis.Add(apiEndpoint);
            return listofApis;

        }

        

        #endregion
    }
}
