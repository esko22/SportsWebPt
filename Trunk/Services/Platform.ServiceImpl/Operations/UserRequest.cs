using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.ServiceContracts.Models;

namespace SportsWebPt.Platform.ServiceImpl.Operations
{
    public class UserRequest : BaseResourceRequest
    {
        #region Properties

        //TODO: this is hack... need to have a resource add base

        public UserDto User { get; set; }

        #endregion
    }
}
