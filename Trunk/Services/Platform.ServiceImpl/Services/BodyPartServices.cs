using System.Collections.Generic;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

using System.Linq;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class BodyPartService : RestService
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(BodyPartListRequest request)
        {
            var areaComponents = new List<BodyPart>();

            if (request.SkeletonAreaId == 0)
                areaComponents.AddRange(SkeletonUnitOfWork.BodyPartRepo.GetAll());
            else
                areaComponents = SkeletonUnitOfWork.BodyPartMatrixRepo.GetAll().Where(s => s.SkeletonAreaId == request.SkeletonAreaId).Select(p => p.BodyPart).ToList();

            var responseList = new List<BodyPartDto>();
            Mapper.Map(areaComponents.OrderBy(o => o.CommonName), responseList);

            return
                Ok(new ApiListResponse<BodyPartDto, BodyPartSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }


        public object Post(CreateBodyPartRequest request)
        {
            Check.Argument.IsNotNull(request, "BodyPartDto");

            var bodyPart = Mapper.Map<BodyPart>(request);

            bodyPart.SecondaryBodyPartMatrix.ForEach(p =>
                {
                    p.IsSecondary = true;
                    bodyPart.BodyPartMatrix.Add(p);
                });

            SkeletonUnitOfWork.BodyPartRepo.Add(bodyPart);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyPartDto>(request));
        }

        public object Put(UpdateBodyPartRequest request)
        {
            Check.Argument.IsNotNull(request, "BodyPartDto");

            var bodyPart = Mapper.Map<BodyPart>(request);
            bodyPart.SecondaryBodyPartMatrix.ForEach(p =>
            {
                p.IsSecondary = true;
                bodyPart.BodyPartMatrix.Add(p);
            });

            bodyPart.BodyPartMatrix.ForEach(f => f.BodyPartId = bodyPart.Id);

            SkeletonUnitOfWork.UpdateBodyPart(bodyPart);
            SkeletonUnitOfWork.Commit();

            return Ok(new ApiResponse<BodyPartDto>(request));
        }


        public object Get(BodyPartMatrixListRequest request)
        {
            var bodyPartMatrixItems = SkeletonUnitOfWork.GetBodyPartMatrixItems();

            var responseList = new List<BodyPartMatrixItemDto>();

            Mapper.Map(bodyPartMatrixItems, responseList);

            return
                Ok(new ApiListResponse<BodyPartMatrixItemDto, BodyPartSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        #endregion


    }


}
