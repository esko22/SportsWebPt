using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SymptomService : LoggingRestServiceBase<SymptomListRequest, ListResponse<SymptomDto, BasicSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(SymptomListRequest request)
        {
            var symptoms = new List<Symptom>();

            if (request.componentId == 0)
                symptoms.AddRange(SkeletonUnitOfWork.SymptomRepo.GetAll());
            else
            {
                var component = SkeletonUnitOfWork.AreaComponentRepo.GetAll().SingleOrDefault(p => p.Id == request.componentId);
                if (component != null)
                    symptoms = component.Symptoms.ToList();
            }

            var responseList = new List<SymptomDto>();
            Mapper.Map(symptoms, responseList);

            return
                Ok(new ListResponse<SymptomDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }
}
