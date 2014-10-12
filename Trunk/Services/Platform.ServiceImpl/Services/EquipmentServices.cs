using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{

    public class EquipmentService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(EquipmentListRequest request)
        {
            var responseList = new List<EquipmentDto>();
            Mapper.Map(ResearchUnitOfWork.EquipmentRepo.GetAll().OrderBy(p => p.CommonName), responseList);

            return
                Ok(new ApiListResponse<EquipmentDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(BriefEquipmentListRequest request)
        {
            Thread.Sleep(3000);
            var responseList = new List<BriefEquipmentDto>();
            Mapper.Map(ResearchUnitOfWork.EquipmentRepo.GetAll().OrderBy(p => p.Id), responseList);

            return
                Ok(new ApiListResponse<BriefEquipmentDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Post(CreateEquipmentRequest request)
        {
            Check.Argument.IsNotNull(request, "EquipmentDto");

            var equipment = Mapper.Map<Equipment>(request);
            
            ResearchUnitOfWork.EquipmentRepo.Add(equipment);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<EquipmentDto>(request));
        }

        public object Put(UpdateEquipmentRequest request)
        {
            Check.Argument.IsNotNull(request, "EquipmentDto");

            var equipment = Mapper.Map<Equipment>(request);

            ResearchUnitOfWork.EquipmentRepo.Update(equipment);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<EquipmentDto>(request));
        }

        #endregion
    }
}
