using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.Repositories
{
    public class EpisodeRepo : EFRepository<Episode>,IEpisodeRepo
    {
        #region Construction

        public EpisodeRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public IQueryable<Episode> GetEpisodeDetails()
        {
            return GetAll()
                .Include(i => i.Therapist.User)
                .Include(i => i.Patient)
                .Include(i => i.Clinic)
                .Include(i => i.Prognosis);
        }

        #endregion

    }

    public interface IEpisodeRepo : IRepository<Episode>
    {
        IQueryable<Episode> GetEpisodeDetails();
    }
}
