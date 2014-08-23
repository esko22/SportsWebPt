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
    public class EpisodeUnitOfWork : BaseUnitOfWork, IEpisodeUnitOfWork
    {
        #region Properties

        public IEpisodeRepo EpisodeRepo { get { return GetRepo<IEpisodeRepo>(); } }
        public IRepository<Session> SessionRepo { get { return GetStandardRepo<Session>(); } }

        #endregion

        #region Construction

        public EpisodeUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public IQueryable<Episode> GetFilteredEpisodes(int therapistId, int patientId, string state)
        {
            var episodes = EpisodeRepo.GetEpisodeDetails();
            var predicate = PredicateBuilder.True<Episode>();

            if (patientId > 0)
                predicate = predicate.And(p => p.Patient.Id == patientId);

            if (therapistId > 0)
                predicate = predicate.And(p => p.Therapist.Id == therapistId);

            if (!String.IsNullOrEmpty(state))
            {
                var episodeState = (EpisodeState)Enum.Parse(typeof(EpisodeState), state, true);
                predicate = predicate.And(p => p.State == episodeState);
            }
            return episodes.AsExpandable().Where(predicate);
        }

        public IQueryable<Session> GetEpisodeSessions()
        {
            return SessionRepo.GetAll()
                .Include(p => p.SessionPlans)
                .Include(p => p.ScheduledWith.User);
        }



        #endregion

    }

    public interface IEpisodeUnitOfWork :  IBaseUnitOfWork
    {
        IEpisodeRepo EpisodeRepo { get; }

        IQueryable<Episode> GetFilteredEpisodes(int therapistId = 0, int patientId = 0, string state = "");
        IQueryable<Session> GetEpisodeSessions();
    }
}
