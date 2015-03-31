using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class VideoService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(VideoListRequest request)
        {
            var responseList = new List<VideoDto>();
            Mapper.Map(ResearchUnitOfWork.VideoRepo.GetAll().OrderBy(o => o.Name) , responseList);

            return
                Ok(new ApiListResponse<VideoDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Post(CreateVideoRequest request)
        {
            Check.Argument.IsNotNull(request, "VideoDto");

            var video = Mapper.Map<Video>(request);

            ResearchUnitOfWork.VideoRepo.Add(video);
            ResearchUnitOfWork.Commit();

            request.Id = video.Id;

            return Ok(new ApiResponse<VideoDto>(request));
        }

        public object Put(UpdateVideoRequest request)
        {
            Check.Argument.IsNotNull(request, "VideoDto");

            ResearchUnitOfWork.UpdateVideo(Mapper.Map<Video>(request));

            return Ok(new ApiResponse<VideoDto>(request));
        }

        #endregion
    }
}
