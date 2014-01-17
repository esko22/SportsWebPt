using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/filters", "GET")]
    public class FilterListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<FilterDto, BasicSortBy>>
    {
        #region Properties

        public FilterTypeDto? FilterType { get; set; }

        #endregion
    }

}
