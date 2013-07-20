using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Core.Models
{
    public class PlanBodyRegionMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int PlanId { get; set; }

        public int BodyRegionId { get; set; }

        public virtual Plan Plan { get; set; }

        public virtual BodyRegion BodyRegion { get; set; }

        #endregion

    }
}
