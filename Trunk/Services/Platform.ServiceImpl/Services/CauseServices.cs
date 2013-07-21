using System.Collections.Generic;
using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class CauseListService : LoggingRestServiceBase<CauseListRequest, ListResponse<CauseDto, BasicSortBy>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(CauseListRequest request)
        {
            var responseList = new List<CauseDto>();
            Mapper.Map(ResearchUnitOfWork.CauseRepo.GetAll(), responseList);

            return
                Ok(new ListResponse<CauseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class CauseService : LoggingRestServiceBase<CauseRequest, ApiResponse<CauseDto>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(CauseRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "CauseDto");

            var cause = Mapper.Map<Cause>(request.Resource);

            ResearchUnitOfWork.CauseRepo.Add(cause);
            ResearchUnitOfWork.Commit();

            request.Resource.id = cause.Id;

            return Ok(new ApiResponse<CauseDto>(request.Resource));

        }

        public override object OnPut(CauseRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "CauseDto");
            var cause = Mapper.Map<Cause>(request.Resource);

            ResearchUnitOfWork.CauseRepo.Update(cause);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<CauseDto>(request.Resource));
        }

        #endregion
    }
}
