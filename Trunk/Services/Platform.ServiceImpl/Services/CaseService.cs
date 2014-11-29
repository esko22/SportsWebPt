using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ServiceStack.Redis;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class CaseService : RestService
    {

        #region Properties

        public ICaseUnitOfWork CaseUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(CaseListRequest request)
        {
            var responseList = new List<CaseDto>();
            Mapper.Map(CaseUnitOfWork.GetFilteredCases(request.TherapistId, request.PatientId, request.State.ToString()), responseList);

            return
                Ok(new ApiListResponse<CaseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(CaseRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsLong, "Case Id must be given");

            var caseInstance = CaseUnitOfWork.CaseRepo.GetCaseDetails()
                .SingleOrDefault(p => p.Id == request.IdAsLong);

            if (caseInstance == null)
                return NotFound("Case Not Found");

            return Ok(new ApiResponse<CaseDto>(Mapper.Map<CaseDto>(caseInstance)));
        }

        public object Get(CaseSessionListRequest request)
        {
            var responseList = new List<SessionDto>();

            Mapper.Map(CaseUnitOfWork.GetCaseSessions().Where(p => p.CaseId == request.IdAsLong), responseList);

            return
                Ok(new ApiListResponse<SessionDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Post(CreateCaseRequest request)
        {
            Check.Argument.IsNotNull(request, "Session Cannot Be Null");
            Check.Argument.IsNotNegativeOrZero(request.ClinicId, "Clinic Id");
            Check.Argument.IsNotNegativeOrZero(request.ClinicPatientId, "Clinic Patient Id");
            Check.Argument.IsNotNullOrEmpty(request.TherapistId, "Therapist Id");


            var caseInstance = Mapper.Map<Case>(request);
            caseInstance.PrognosisId = null;
            caseInstance.State = CaseState.Active;
            caseInstance.CreatedOn = DateTime.Now;

            CaseUnitOfWork.CaseRepo.Add(caseInstance);
            CaseUnitOfWork.Commit();

            return Ok(new ApiResponse<CaseDto>() { Response = Mapper.Map<CaseDto>(caseInstance) });
        }


        #endregion

    }
}
