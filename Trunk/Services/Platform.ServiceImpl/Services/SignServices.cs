using System.Collections.Generic;
using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SignListService : LoggingRestServiceBase<SignListRequest, ListResponse<SignDto, BasicSortBy>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(SignListRequest request)
        {
            var responseList = new List<SignDto>();
            Mapper.Map(ResearchUnitOfWork.SignRepo.GetAll(), responseList);

            return
                Ok(new ListResponse<SignDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }

    public class SignService : LoggingRestServiceBase<SignRequest, ApiResponse<SignDto>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(SignRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "SignDto");

            var sign = Mapper.Map<Sign>(request.Resource);

            ResearchUnitOfWork.SignRepo.Add(sign);
            ResearchUnitOfWork.Commit();

            request.Resource.id = sign.Id;

            return Ok(new ApiResponse<SignDto>(request.Resource));

        }

        public override object OnPut(SignRequest request)
        {
            Check.Argument.IsNotNull(request.Resource, "SignDto");
            var sign = Mapper.Map<Sign>(request.Resource);

            ResearchUnitOfWork.SignRepo.Update(sign);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<SignDto>(request.Resource));
        }

        #endregion
    }
}
