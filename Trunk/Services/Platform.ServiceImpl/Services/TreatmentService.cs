using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class TreatmentService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(TreatmentListRequest request)
        {
            var responseList = new List<TreatmentDto>();
            Mapper.Map(ResearchUnitOfWork.TreatmentRepo.GetAll().OrderBy(p => p.Id), responseList);

            return
                Ok(new ApiListResponse<TreatmentDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        public object Post(CreateTreatmentRequest request)
        {
            Check.Argument.IsNotNull(request, "TreatmentDto");

            var treatment = Mapper.Map<Treatment>(request);

            ResearchUnitOfWork.TreatmentRepo.Add(treatment);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<TreatmentDto>(request));
        }

        public object Put(UpdateTreatmentRequest request)
        {
            Check.Argument.IsNotNull(request, "TreatmentDto");

            var treatment = Mapper.Map<Treatment>(request);

            ResearchUnitOfWork.TreatmentRepo.Update(treatment);
            ResearchUnitOfWork.Commit();

            return Ok(new ApiResponse<TreatmentDto>(request));
        }

        #endregion
    }
}
