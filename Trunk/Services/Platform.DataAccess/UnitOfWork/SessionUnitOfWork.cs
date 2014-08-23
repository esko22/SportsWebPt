using System;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SessionUnitOfWork : BaseUnitOfWork, ISessionUnitOfWork
    {
        #region Properties

        public IRepository<Session> SessionRepo { get { return GetStandardRepo<Session>(); } }
        public IRepository<Episode> EpisodeRepo { get { return GetStandardRepo<Episode>(); } } 

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
            Check.Argument.IsNotNegativeOrZero(session.EpisodeId, "EpisodeId");
            Check.Argument.IsNotNegativeOrZero(session.ScheduledWithId, "Scheduled With Id");

            session.Created = DateTime.Now;
            session.ScheduledAt = DateTime.Now;
            session.Executed = DateTime.Now;

            //TODO: hack
            session.DifferentialDiagnosisId = null;

            SessionRepo.Add(session);
            Commit();

            return session;
        } 

        #endregion
    }

    public interface ISessionUnitOfWork : IBaseUnitOfWork
    {
        Session AddSession(Session session);
    }
}
