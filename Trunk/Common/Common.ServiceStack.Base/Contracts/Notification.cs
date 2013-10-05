using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack
{    
    [DataContract]
    public class Notification : INotification<object>
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public object Item { get; set; }
    }
}
