using System;
using System.Data.Entity;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class BodyPartRepo : EFRepository<BodyPart>, IBodyPartRepo
    {
        #region Construction

        public BodyPartRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public override IQueryable<BodyPart> GetAll()
        {
            return base.GetAll()
                .Include(i => i.BodyPartMatrix.Select(l2 => l2.SkeletonArea.Region))
                .Include(i => i.BodyPartMatrix.Select(l2 => l2.SkeletonArea.Side))
                .Include(i => i.BodyPartMatrix.Select(l2 => l2.SkeletonArea.Orientation));
        }

        #endregion
    }

    public interface IBodyPartRepo : IRepository<BodyPart>
    {
    }
}
