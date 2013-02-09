using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    [DataContract]
    public class SwaggerOperationRequest
    {
        [DataMember]
        public string resource { get; set; }
    }
}
