using System;
using System.Collections.Generic;
using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
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

    public class EquipmentService : LoggingRestServiceBase<EquipmentRequest, ApiResponse<EquipmentDto>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(EquipmentRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "EquipmentDto");

            var equipment = Mapper.Map<Equipment>(request.Resource);
            
            ResearchUnitOfWork.EquipmentRepo.Add(equipment);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<EquipmentDto>(request.Resource));

        }

        public override object OnPut(EquipmentRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "EquipmentDto");

            var equipment = Mapper.Map<Equipment>(request.Resource);

            ResearchUnitOfWork.EquipmentRepo.Update(equipment);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<EquipmentDto>(request.Resource));
        }

        #endregion
    }
}
