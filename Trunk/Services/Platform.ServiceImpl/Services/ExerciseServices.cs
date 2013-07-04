using System.Collections.Generic;

using AutoMapper;
using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.DataAccess.UnitOfWork;
using SportsWebPt.Platform.ServiceImpl.Operations;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class ExerciseListService : LoggingRestServiceBase<ExecerciseListRequest, ListResponse<ExerciseDto, BasicSortBy>>
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(ExecerciseListRequest request)
        {
            var responseList = new List<ExerciseDto>();
            Mapper.Map(ResearchUnitOfWork.ExerciseRepo.GetAll(), responseList);

            return
                Ok(new ListResponse<ExerciseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        #endregion
    }
}
