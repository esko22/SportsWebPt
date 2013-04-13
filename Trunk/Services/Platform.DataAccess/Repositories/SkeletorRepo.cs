using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.Repositories
{
    public class SkeletorRepo: EFRepository<SkeletonHotspot>
    {

        #region Construction

        public SkeletorRepo(DbContext context) : base(context) { }

        #endregion

        #region Methods

        public override IQueryable<SkeletonHotspot> GetAll()
        {
            return base.GetAll().Include("Region");
        }

        #endregion
    }
}
