using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class VideoRepo: EFRepository<Video>, IVideoRepo
    {
        #region Construction

        public VideoRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public override IQueryable<Video> GetAll()
        {
            return base.GetAll()
                .Include(i => i.VideoCategoryMatrixItems);
        }

        #endregion
    }

    public interface IVideoRepo : IRepository<Video>
    {
    }
}
