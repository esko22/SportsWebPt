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
        public IUserRepo UserRepo { get { return GetRepo<IUserRepo>(); } }
        public IRepository<Session> SessionRepo { get { return GetStandardRepo<Session>(); } }

        #endregion

        #region Construction

        public EpisodeUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public IQueryable<Episode> GetFilteredEpisodes(String therapistId, String clinicPatientId, string state)
        {
            var episodes = EpisodeRepo.GetEpisodeDetails();
            var predicate = PredicateBuilder.True<Episode>();

            if (!String.IsNullOrEmpty(clinicPatientId))
                predicate = predicate.And(p => p.ClinicPatient.UserId == new Guid(clinicPatientId));

            if (!String.IsNullOrEmpty(therapistId))
                predicate = predicate.And(p => p.Therapist.User.Id == new Guid(therapistId));

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
        IUserRepo UserRepo { get; }

        IQueryable<Episode> GetFilteredEpisodes(string therapistId = "", string patientId = "", string state = "");
        IQueryable<Session> GetEpisodeSessions();
    }
}
