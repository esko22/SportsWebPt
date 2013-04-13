using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    [ApiResource("User CRUD endpoint", "skeletor", "/operations?resource=skeletor")]
    public class SkeletorService : LoggingRestServiceBase<SkeletorHotspotListRequest, ListResponse<SkeletorHotspotDto, SkeletorSortBy>>
    {
        #region Properties

        public ISkeletorUnitOfWork SkeletorUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(SkeletorHotspotListRequest request)
        {
            var skeletorHotspots = SkeletorUnitOfWork.SkeletonHotspotRepo.GetAll();
            var responseList = new List<SkeletorHotspotDto>();

            Mapper.Map(skeletorHotspots, responseList);

            return
                Ok(new ListResponse<SkeletorHotspotDto, SkeletorSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
