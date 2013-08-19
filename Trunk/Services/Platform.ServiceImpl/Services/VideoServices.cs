using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class VideoListService : LoggingRestServiceBase<VideoListRequest, ListResponse<VideoDto, BasicSortBy>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(VideoListRequest request)
        {
            var responseList = new List<VideoDto>();
            Mapper.Map(ResearchUnitOfWork.VideoRepo.GetAll(), responseList);

            return
                Ok(new ListResponse<VideoDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class VideoService : LoggingRestServiceBase<VideoRequest, ApiResponse<VideoDto>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(VideoRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "VideoDto");

            var video = Mapper.Map<Video>(request.Resource);

            ResearchUnitOfWork.VideoRepo.Add(video);
            ResearchUnitOfWork.Commit();

            request.Resource.Id = video.Id;

            return Ok(new ApiResponse<VideoDto>(request.Resource));
        }

        public override object OnPut(VideoRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "VideoDto");

            var video = Mapper.Map<Video>(request.Resource);

            ResearchUnitOfWork.VideoRepo.Update(video);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<VideoDto>(request.Resource));
        }

        #endregion
    }
}
