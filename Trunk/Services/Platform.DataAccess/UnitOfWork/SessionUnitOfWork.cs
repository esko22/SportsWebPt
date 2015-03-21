using System;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess.Repositories;

namespace SportsWebPt.Platform.DataAccess
{
    public class SessionUnitOfWork : BaseUnitOfWork, ISessionUnitOfWork
    {
        #region Properties

        public ISessionRepo SessionRepo { get { return GetRepo<ISessionRepo>(); } }
        public IRepository<Case> CaseRepo { get { return GetStandardRepo<Case>(); } }
        public IRepository<SessionPlanMatrixItem> SessionPlanMatrixRepo { get { return GetStandardRepo<SessionPlanMatrixItem>();} }
        public IRepository<TherapistPlanMatrixItem> TherapistPlanMatrixRepo { get {  return GetStandardRepo<TherapistPlanMatrixItem>();} } 

        #endregion

        #region Construction

        public SessionUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods
        
        public Session AddSession(Session session)
        {
            Check.Argument.IsNotNull(session, "Session Cannot Be Null");
            Check.Argument.IsNotNegativeOrZero(session.CaseId, "CaseId");
            Check.Argument.IsNotEmpty(session.ScheduledWithId, "Scheduled With Id");

            session.Created = DateTime.Now;
            session.Executed = null;
            //TODO: hack
            session.DifferentialDiagnosisId = null;

            SessionRepo.Add(session);
            Commit();

            return session;
        }

        public Session GetSession(Int64 id)
        {
            var session =  SessionRepo.GetSessionDetails().SingleOrDefault(p => p.Id == id);

            if (session != null)
            {
                session.SessionPlans.ForEach(f =>
                {
                    f.Plan.TherapistPlanMatrixItems =
                        TherapistPlanMatrixRepo.GetAll().Where(p => p.PlanId == f.PlanId).ToList();
                });
            }

            return session;
        }

        public Session GetSessionWithPlanOwner(Int64 id, Guid therapistId)
        {
            var session = GetSession(id);

            if (session != null)
            {
                session.SessionPlans.ForEach(f =>
                {
                    f.Plan.TherapistPlanMatrixItems =
                        TherapistPlanMatrixRepo.GetAll().Where(p => p.PlanId == f.PlanId && p.TherapistId == therapistId).ToList();
                });
            }

            return session;
        }


        public Session UpdateSession(Session session)
        {
            Check.Argument.IsNotNull(session, "Session Cannot Be Null");
            Check.Argument.IsNotNegativeOrZero(session.Id, "SessionId");

            var sessionInDb = SessionRepo.GetSessionDetails()
                .SingleOrDefault(p => p.Id == session.Id);

            if (sessionInDb == null)
                throw new ArgumentNullException("session id", "session does not exist");

            var sessionEntry = _context.Entry(sessionInDb);
            sessionEntry.CurrentValues.SetValues(session);

            Commit();

            return session;
        }

        public void SetSessionPlans(Int64 sessionId, int[] planIds)
        {
            Check.Argument.IsNotNegativeOrZero(sessionId, "SessionId");

            var sessionPlans = SessionPlanMatrixRepo.GetAll().Where(p => p.SessionId == sessionId);

            if (sessionPlans.Any())
            {
                foreach (var sessionPlan in sessionPlans)
                    SessionPlanMatrixRepo.Delete(sessionPlan);

                Commit();
            }

            foreach (int planId in planIds)
                SessionPlanMatrixRepo.Add(new SessionPlanMatrixItem() { SessionId = sessionId, PlanId = planId, Name = String.Empty} );    

            Commit();
        }


        #endregion
    }

    public interface ISessionUnitOfWork : IBaseUnitOfWork
    {
        ISessionRepo SessionRepo { get; }

        Session GetSession(Int64 id);
        Session GetSessionWithPlanOwner(Int64 id, Guid therapistId);
        Session UpdateSession(Session session);
        Session AddSession(Session session);
        void SetSessionPlans(Int64 sessionId, int[] planIds);
    }
}
