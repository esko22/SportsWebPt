using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PatientService : RestService
    {

        #region Properties

        public ICaseUnitOfWork CaseUnitOfWork { get; set; }

        #endregion


        #region Methods

        public object Get(PatientCaseListRequest request)
        {
            var responseList = new List<CaseDto>();
            var cases = CaseUnitOfWork.GetFilteredCases(patientId: request.Id, state: request.State.ToString());

            Mapper.Map(cases, responseList);

            return
                Ok(new ApiListResponse<CaseDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        #endregion
    }
}
