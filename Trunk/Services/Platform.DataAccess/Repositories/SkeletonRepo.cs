using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SkeletonRepo: EFRepository<SkeletonHotspot>
    {

        #region Construction

        public SkeletonRepo(DbContext context) : base(context) { }

        #endregion

        #region Methods

        public override IQueryable<SkeletonHotspot> GetAll()
        {
            return base.GetAll()
                .Include("Region")
                .Include("Side")
                .Include("Orientation");
        }

        #endregion
    }
}
