using System.Collections.Generic;

using System.Linq;
using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    [ApiResource("User CRUD endpoint", "skeletor", "/operations?resource=skeletor")]
    public class SkeletorService : LoggingRestServiceBase<SkeletorHotspotDto, ListResponse<SkeletorHotspotDto, SkeletorSortBy>>
    {
        #region Properties

        public ISkeletorUnitOfWork SkeletorUnitOfWork { get; set; }

        #endregion


        #region Methods

        public override object OnGet(SkeletorHotspotDto request)
        {
            var skeletorHotspots = SkeletorUnitOfWork.SkeletonHotspotRepo.GetAll(new List<string>() { "Region", "Side", "Orientation"});
            var responseList = new List<SkeletorHotspotDto>();

            skeletorHotspots.ForEach(s => responseList.Add(new SkeletorHotspotDto()
                {
                    Id = s.Id,
                    Orientation = s.Orientation.Orientation,
                    Region = s.Region.Region,
                    Side = s.Side.Side
                }));

            return
                Ok(new ListResponse<SkeletorHotspotDto, SkeletorSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
