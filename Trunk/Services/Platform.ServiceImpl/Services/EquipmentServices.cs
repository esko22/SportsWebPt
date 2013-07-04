using System.Collections.Generic;
using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.DataAccess.UnitOfWork;
using SportsWebPt.Platform.ServiceImpl.Operations;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class EquipmentListService : LoggingRestServiceBase<EquipmentListRequest, ListResponse<EquipmentDto, BasicSortBy>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(EquipmentListRequest request)
        {
            var responseList = new List<EquipmentDto>();
            Mapper.Map(ResearchUnitOfWork.EquipmentRepo.GetAll(), responseList);

            return
                Ok(new ListResponse<EquipmentDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }
}
