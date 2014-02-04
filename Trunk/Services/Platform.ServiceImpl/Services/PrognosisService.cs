using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PrognosisService : RestService
    {

        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(PrognosisListRequest request)
        {
            var responseList = new List<PrognosisDto>();
            Mapper.Map(ResearchUnitOfWork.PrognosisRepo.GetAll().OrderBy(p => p.Id), responseList);

            return
                Ok(new ApiListResponse<PrognosisDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Post(CreatePrognosisRequest request)
        {
            Check.Argument.IsNotNull(request, "PrognosisDto");

            var prognosis = Mapper.Map<Prognosis>(request);

            ResearchUnitOfWork.PrognosisRepo.Add(prognosis);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<PrognosisDto>(request));
        }

        public object Put(UpdatePrognosisRequest request)
        {
            Check.Argument.IsNotNull(request, "PrognosisDto");

            var prognosis = Mapper.Map<Prognosis>(request);

            ResearchUnitOfWork.PrognosisRepo.Update(prognosis);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<PrognosisDto>(request));
        }

        #endregion
    }
}
