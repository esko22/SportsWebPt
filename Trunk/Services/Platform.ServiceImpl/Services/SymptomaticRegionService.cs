using System.Collections.Generic;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SymptomaticRegionService : RestService
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(SymptomaticRegionListRequest request)
        {
            var areas = SkeletonUnitOfWork.SkeletonAreaRepo.GetSymptonmaticRegions();
            var responseList = new List<SymptomaticRegionDto>();

            Mapper.Map(areas, responseList);

            return
                Ok(new ApiListResponse<SymptomaticRegionDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }
}
