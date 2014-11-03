using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot.Relational;

namespace SportsWebPt.Identity.Core
{
    public class SportsWebUser: RelationalUserAccount
    {
        #region Properties

        public virtual string ServiceAccount { get; set; }

        #endregion
    }
}
