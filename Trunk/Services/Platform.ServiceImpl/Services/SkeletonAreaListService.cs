using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SkeletonAreaListService : RestService
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(SkeletonAreaListRequest request)
        {
            var skeletonAreas = SkeletonUnitOfWork.SkeletonAreaRepo.GetAll();
            var responseList = new List<SkeletonAreaDto>();

            Mapper.Map(skeletonAreas, responseList);

            return
                Ok(new ApiListResponse<SkeletonAreaDto, SkeletonSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
