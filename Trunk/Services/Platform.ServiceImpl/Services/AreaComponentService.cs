using System.Collections.Generic;
using AutoMapper;
using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

using System.Linq;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class AreaComponentService : LoggingRestServiceBase<AreaComponentListRequest, ListResponse<AreaComponentDto, AreaComponentSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(AreaComponentListRequest request)
        {
            var areaComponents = new List<AreaComponent>();

            if (request.areaId == 0)
                areaComponents.AddRange(SkeletonUnitOfWork.AreaComponentRepo.GetAll());
            else
            {
                var skeltonArea = SkeletonUnitOfWork.SkeletonAreaRepo.GetAll().SingleOrDefault(p => p.Id == request.areaId);
                if (skeltonArea != null)
                    areaComponents = skeltonArea.Components.ToList();
            }

            var responseList = new List<AreaComponentDto>();
            Mapper.Map(areaComponents, responseList);

            return
                Ok(new ListResponse<AreaComponentDto, AreaComponentSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }
}
