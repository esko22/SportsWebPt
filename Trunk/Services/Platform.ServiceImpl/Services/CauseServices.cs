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
    public class CauseService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(CauseListRequest request)
        {
            var responseList = new List<CauseDto>();
            Mapper.Map(ResearchUnitOfWork.CauseRepo.GetAll().OrderBy(o => o.Description), responseList);

            return
                Ok(new ApiListResponse<CauseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Post(CreateCauseRequest request)
        {
            Check.Argument.IsNotNull(request, "CauseDto");

            var cause = Mapper.Map<Cause>(request);

            ResearchUnitOfWork.CauseRepo.Add(cause);
            ResearchUnitOfWork.Commit();

            request.Id = cause.Id;

            return Ok(new ApiResponse<CauseDto>(request));
        }

        public object Put(UpdateCauseRequest request)
        {
            Check.Argument.IsNotNull(request, "CauseDto");
            var cause = Mapper.Map<Cause>(request);

            ResearchUnitOfWork.CauseRepo.Update(cause);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<CauseDto>(request));
        }

        #endregion
    }
}
