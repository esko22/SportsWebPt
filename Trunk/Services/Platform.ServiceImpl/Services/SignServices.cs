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

    public class SignService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(SignListRequest request)
        {
            var responseList = new List<SignDto>();
            Mapper.Map(ResearchUnitOfWork.SignRepo.GetAll().OrderBy(p => p.Description).OrderBy(c => c.Category), responseList);

            return
                Ok(new ApiListResponse<SignDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Post(CreateSignRequest request)
        {
            Check.Argument.IsNotNull(request, "SignDto");

            var sign = Mapper.Map<Sign>(request);

            ResearchUnitOfWork.SignRepo.Add(sign);
            ResearchUnitOfWork.Commit();

            request.Id = sign.Id;

            return Ok(new ApiResponse<SignDto>(request));

        }

        public object Put(UpdateSignRequest request)
        {
            Check.Argument.IsNotNull(request, "SignDto");
            var sign = Mapper.Map<Sign>(request);

            ResearchUnitOfWork.SignRepo.Update(sign);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<SignDto>(request));
        }

        #endregion
    }
}
