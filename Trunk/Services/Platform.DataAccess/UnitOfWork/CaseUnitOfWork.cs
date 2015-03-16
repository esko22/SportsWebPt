using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess.Repositories;

namespace SportsWebPt.Platform.DataAccess
{
    public class CaseUnitOfWork : BaseUnitOfWork, ICaseUnitOfWork
    {
        #region Properties

        public ICaseRepo CaseRepo { get { return GetRepo<ICaseRepo>(); } }
        public IUserRepo UserRepo { get { return GetRepo<IUserRepo>(); } }
        public IRepository<Session> SessionRepo { get { return GetStandardRepo<Session>(); } }

        #endregion

        #region Construction

        public CaseUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public IQueryable<Case> GetFilteredCases(String therapistId, String clinicPatientId, string state)
        {
            var cases = CaseRepo.GetCaseDetails();
            var predicate = PredicateBuilder.True<Case>();

            if (!String.IsNullOrEmpty(clinicPatientId))
                predicate = predicate.And(p => p.ClinicPatient.UserId == new Guid(clinicPatientId));

            if (!String.IsNullOrEmpty(therapistId))
                predicate = predicate.And(p => p.Therapist.User.Id == new Guid(therapistId));

            if (!String.IsNullOrEmpty(state))
            {
                var caseState = (CaseState)Enum.Parse(typeof(CaseState), state, true);
                predicate = predicate.And(p => p.State == caseState);
            }
            return cases.AsExpandable().Where(predicate);
        }

        public IQueryable<Session> GetCaseSessions()
        {
            return SessionRepo.GetAll()
                .Include(p => p.SessionPlans)
                .Include(p => p.ScheduledWith.User);
        }

        public IQueryable<Session> GetCaseSessionsWithPlans()
        {
            return GetCaseSessions().Include(i => i.SessionPlans.Select(i2 => i2.Plan));
        } 

        #endregion

    }

    public interface ICaseUnitOfWork :  IBaseUnitOfWork
    {
        ICaseRepo CaseRepo { get; }
        IUserRepo UserRepo { get; }

        IQueryable<Case> GetFilteredCases(string therapistId = "", string patientId = "", string state = "");
        IQueryable<Session> GetCaseSessions();
        IQueryable<Session> GetCaseSessionsWithPlans();
    }
}
