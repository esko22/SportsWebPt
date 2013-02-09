using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    [DataContract]
    public class SwaggerResourceRequest
    {
        [DataMember]
        public string resource { get; set; }
    }
}
