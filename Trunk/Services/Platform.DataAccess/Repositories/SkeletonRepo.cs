using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SkeletonRepo: EFRepository<SkeletonArea>, ISkeletonRepo
    {

        #region Construction

        public SkeletonRepo(DbContext context) : base(context) { }

        #endregion

        #region Methods

        public override IQueryable<SkeletonArea> GetAll()
        {
            //NOTE: this is set here to prevent adding a reference to Data.Entities in the service impls
            return base.GetAll()
                       .Include("Region")
                       .Include("Side")
                       .Include("Orientation");
        }

        public IQueryable<SkeletonArea> GetSymptonmaticRegions()
        {
            return base.GetAll()
                       .Include("Region")
                       .Include("Side")
                       .Include("Orientation")
                       .Include("BodyPartMatrix.BodyPart");
        }

        #endregion
    }
}
