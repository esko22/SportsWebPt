using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class BodyRegionListService : LoggingRestServiceBase<BodyRegionListRequest, ListResponse<BodyRegionDto, BasicSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(BodyRegionListRequest request)
        {

            var responseList = new List<BodyRegionDto>();
            Mapper.Map(SkeletonUnitOfWork.BodyRegionRepo.GetAll(), responseList);

            return
                Ok(new ListResponse<BodyRegionDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class BodyRegionService : LoggingRestServiceBase<BodyRegionRequest, ApiResponse<BodyRegionDto>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(BodyRegionRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "BodyRegionDto");

            var bodyRegion = Mapper.Map<BodyRegion>(request.Resource);

            SkeletonUnitOfWork.BodyRegionRepo.Add(bodyRegion);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyRegionDto>(request.Resource));
        }

        public override object OnPut(BodyRegionRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "BodyRegionDto");

            var bodyRegion = Mapper.Map<BodyRegion>(request.Resource);

            SkeletonUnitOfWork.BodyRegionRepo.Update(bodyRegion);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyRegionDto>(request.Resource));
        }

        #endregion

    }
}
