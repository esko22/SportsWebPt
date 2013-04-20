using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    [ApiResource("User CRUD endpoint", "Skeleton", "/operations?resource=Skeleton")]
    public class SkeletonService : LoggingRestServiceBase<SkeletonHotspotListRequest, ListResponse<SkeletonHotspotDto, SkeletonSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(SkeletonHotspotListRequest request)
        {
            var SkeletonHotspots = SkeletonUnitOfWork.SkeletonHotspotRepo.GetAll();
            var responseList = new List<SkeletonHotspotDto>();

            Mapper.Map(SkeletonHotspots, responseList);

            return
                Ok(new ListResponse<SkeletonHotspotDto, SkeletonSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
