using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class BodyRegionService : RestService
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(BodyRegionListRequest request)
        {
            var responseList = new List<BodyRegionDto>();
            Mapper.Map(SkeletonUnitOfWork.BodyRegionRepo.GetAll().OrderBy(o => o.Name), responseList);

            return
                Ok(new ApiListResponse<BodyRegionDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Post(CreateBodyRegionRequest request)
        {
            Check.Argument.IsNotNull(request, "BodyRegionDto");

            var bodyRegion = Mapper.Map<BodyRegion>(request);

            SkeletonUnitOfWork.BodyRegionRepo.Add(bodyRegion);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyRegionDto>(request));
        }

        public object Put(UpdateBodyRegionRequest request)
        {
            Check.Argument.IsNotNull(request, "BodyRegionDto");

            var bodyRegion = Mapper.Map<BodyRegion>(request);

            SkeletonUnitOfWork.BodyRegionRepo.Update(bodyRegion);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyRegionDto>(request));
        }

        #endregion

    }
}
