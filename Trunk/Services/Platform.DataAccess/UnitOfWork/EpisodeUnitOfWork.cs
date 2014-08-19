using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion

        #region Construction

        public EpisodeUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public IQueryable<Episode> GetFilteredEpisodes(int clinicTherapistId, int clinicPatientId, String state = "")
        {
            var episodes = EpisodeRepo.GetEpisodeDetails();
            var predicate = PredicateBuilder.True<Episode>();

            if (clinicPatientId > 0)
                predicate = predicate.And(p => p.ClinicPatientId == clinicPatientId);

            if (clinicTherapistId > 0)
                predicate = predicate.And(p => p.ClinicTherapistId == clinicTherapistId);

            if (!String.IsNullOrEmpty(state))
            {
                var episodeState = (EpisodeState) Enum.Parse(typeof (EpisodeState), state, true);
                predicate = predicate.And(p => p.State == episodeState);
            }
            return episodes.AsExpandable().Where(predicate);
        }

        #endregion

    }

    public interface IEpisodeUnitOfWork
    {
        IQueryable<Episode> GetFilteredEpisodes(int clinicTherapistId, int clinicPatientId, string state = "");
    }
}
