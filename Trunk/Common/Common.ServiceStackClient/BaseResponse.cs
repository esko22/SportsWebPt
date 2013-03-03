using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.ServiceInterface.ServiceModel;

namespace SportsWebPt.Common.ServiceStackClient
{
    public class BaseResponse<TResponse>
     where TResponse : class
    {
        public virtual TResponse Response { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
