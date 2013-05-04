using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PotentialSymptomListService : LoggingRestServiceBase<PotentialSymptomListRequest, ListResponse<SymptomDto, BasicSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(PotentialSymptomListRequest request)
        {

            var symptoms = request.BodyPartMatrixId == 0
                               ? SkeletonUnitOfWork.SymptomMatrixRepo.GetAll()
                               : SkeletonUnitOfWork.SymptomMatrixRepo.GetPotentialSymptoms(request.BodyPartMatrixId);

            var responseList = new List<PotentialSymptomDto>();
            Mapper.Map(symptoms, responseList);

            return
                Ok(new ListResponse<PotentialSymptomDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }
}
