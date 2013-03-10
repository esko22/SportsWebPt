using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsWebPt.Common.ServiceStack.Infrastructure;

namespace SportsWebPt.Platform.ServiceImpl.Operations
{
    public class UserRequest : BaseResourceRequest
    {
        #region Properties

        //TODO: this is hack... need to have a resource add base

        public String emailAddress { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public String userName { get; set; }

        public String password { get; set; }

        public String phone { get; set; }

        public String skypeHandle { get; set; }

        #endregion
    }
}
