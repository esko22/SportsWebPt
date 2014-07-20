using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SymptomListService : RestService
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(SymptomListRequest request)
        {
            var symptoms = new List<Symptom>();

            symptoms.AddRange(SkeletonUnitOfWork.SymptomRepo.GetAll());

            var responseList = new List<SymptomDto>();
            Mapper.Map(symptoms, responseList);

            return
                Ok(new ApiListResponse<SymptomDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
