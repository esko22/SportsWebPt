using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.DataAccess.UnitOfWork;
using SportsWebPt.Platform.ServiceImpl.Operations;
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
}
