using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjuryBodyRegionMatrixItem
    {

        #region Properties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int BodyRegionId { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual BodyRegion BodyRegion { get; set; }

        #endregion

    }
}
