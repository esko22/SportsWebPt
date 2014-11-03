﻿using System;
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
        public IRepository<Episode> EpisodeRepo { get { return GetStandardRepo<Episode>(); } }
        public IRepository<SessionPlanMatrixItem> SessionPlanMatrixRepo { get { return GetStandardRepo<SessionPlanMatrixItem>();} } 

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

        public void AddSessionPlans(Int64 sessionId, int[] planIds)
        {

            foreach (int planId in planIds)
            {
                SessionPlanMatrixRepo.Add(new SessionPlanMatrixItem() { SessionId = sessionId, PlanId = planId, Name = String.Empty} );    
            }

            Commit();
        }


        #endregion
    }

    public interface ISessionUnitOfWork : IBaseUnitOfWork
    {
        ISessionRepo SessionRepo { get; }

        Session AddSession(Session session);
        void AddSessionPlans(Int64 sessionId, int[] planIds);
    }
}