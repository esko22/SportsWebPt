using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

using System.Linq;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class BodyPartListService : LoggingRestServiceBase<BodyPartListRequest, ListResponse<BodyPartDto, BodyPartSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(BodyPartListRequest request)
        {
            var areaComponents = new List<BodyPart>();

            if (request.skeletonAreaId == 0)
                areaComponents.AddRange(SkeletonUnitOfWork.BodyPartRepo.GetAll());
            else
                areaComponents = SkeletonUnitOfWork.BodyPartMatrixRepo.GetAll().Where(s => s.SkeletonAreaId == request.skeletonAreaId).Select(p => p.BodyPart).ToList();

            var responseList = new List<BodyPartDto>();
            Mapper.Map(areaComponents, responseList);

            return
                Ok(new ListResponse<BodyPartDto, BodyPartSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class BodyPartService : LoggingRestServiceBase<BodyPartRequest, ApiResponse<BodyPartDto>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(BodyPartRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "BodyPartDto");

            var bodyPart = Mapper.Map<BodyPart>(request.Resource);

            SkeletonUnitOfWork.BodyPartRepo.Add(bodyPart);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyPartDto>(request.Resource));
        }

        public override object OnPut(BodyPartRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "BodyPartDto");

            var bodyPart = Mapper.Map<BodyPart>(request.Resource);

            SkeletonUnitOfWork.BodyPartRepo.Update(bodyPart);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyPartDto>(request.Resource));
        }

        #endregion

    }

    public class BodyPartMatrixItemService : LoggingRestServiceBase<BodyPartMatrixListRequest, ListResponse<BodyPartMatrixItemDto, BodyPartSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(BodyPartMatrixListRequest request)
        {
            var bodyPartMatrixItems = SkeletonUnitOfWork.BodyPartMatrixRepo.GetAll(new []
                {
                    "BodyPart",
                    "SkeletonArea",
                    "SkeletonArea.Region",
                    "SkeletonArea.Side",
                    "SkeletonArea.Orientation"
                });

            var responseList = new List<BodyPartMatrixItemDto>();

            Mapper.Map(bodyPartMatrixItems, responseList);

            return
                Ok(new ListResponse<BodyPartMatrixItemDto, BodyPartSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
