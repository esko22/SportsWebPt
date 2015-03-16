using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
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

        public object Get(PatientSnapshotRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Id, "PatientId");

            var activeCases = new List<CaseSnapshotDto>(); 
                
            CaseUnitOfWork.GetFilteredCases(patientId: request.Id, state: CaseState.Active.ToString()).ToList().ForEach(c =>
            {
                var caseSnapshot = Mapper.Map<CaseSnapshotDto>(c);

                caseSnapshot.LastSession =
                    Mapper.Map<SessionDto>(
                        CaseUnitOfWork.GetCaseSessions()
                            .OrderByDescending(o => o.ScheduledStartTime)
                            .FirstOrDefault(f => f.CaseId == c.Id && f.ScheduledStartTime < DateTime.Now));

                caseSnapshot.NextSession =
                    Mapper.Map<SessionDto>(
                        CaseUnitOfWork.GetCaseSessions()
                            .OrderBy(o => o.ScheduledStartTime)
                            .FirstOrDefault(f => f.CaseId == c.Id && f.ScheduledStartTime > DateTime.Now));


                var lastAssignment = CaseUnitOfWork.GetCaseSessionsWithPlans()
                    .OrderBy(o => o.ScheduledStartTime)
                    .FirstOrDefault(f => f.CaseId == c.Id && f.ScheduledStartTime < DateTime.Now && f.SessionPlans.Any());

                if (lastAssignment != null && lastAssignment.SessionPlans != null)
                {
                    var plans = new List<PlanDto>();
                    Mapper.Map(lastAssignment.SessionPlans.Select(s => s.Plan), plans);
                    caseSnapshot.LastAssignment = plans.ToArray();
                }


                activeCases.Add(caseSnapshot);

            });

            var patientSnapshot = new PatientSnapshotDto() {ActiveCases = activeCases.ToArray()};

            return Ok(new ApiResponse<PatientSnapshotDto>(patientSnapshot));
        }

        #endregion
    }
}
