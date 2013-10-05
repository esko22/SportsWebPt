using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/videos", "GET")]
    public class VideoListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<VideoDto, BasicSortBy>> 
    {}

    [Route("/videos", "POST")]
    public class CreateVideoRequest : VideoDto, IReturn<ApiResponse<VideoDto>>
    { }

    [Route("/videos/{id}", "PUT")]
    public class UpdateVideoRequest : VideoDto, IReturn<ApiResponse<VideoDto>>
    { }

    [Route("/videos/{id}", "GET")]
    public class VideoRequest : AbstractResourceRequest, IReturn<ApiResponse<VideoDto>>
    {}

}
