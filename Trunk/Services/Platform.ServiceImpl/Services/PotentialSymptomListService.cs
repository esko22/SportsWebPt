using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PotentialSymptomListService : RestService
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(PotentialSymptomListRequest request)
        {
            var symptoms = request.BodyPartMatrixId == 0
                               ? SkeletonUnitOfWork.SymptomMatrixRepo.GetAll()
                               : SkeletonUnitOfWork.SymptomMatrixRepo.GetPotentialSymptoms(request.BodyPartMatrixId);

            var responseList = new List<PotentialSymptomDto>();
            Mapper.Map(symptoms, responseList);

            return
                Ok(new ApiListResponse<PotentialSymptomDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
