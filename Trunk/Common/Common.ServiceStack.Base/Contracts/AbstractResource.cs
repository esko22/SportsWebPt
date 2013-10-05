using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack
{
    [DataContract(Namespace = "http://sportswebpt")]
    public abstract class AbstractResource
    {
        [DataMember(IsRequired = true)]
        public abstract string Id { get; set; } //NOTE (maxm): This must be redefined in the subclass otherwise serializer puts it at the end of the object

        [DataMember(IsRequired = false)]
        public abstract Uri Href { get; set; }

        public long IdAsLong
        {
            get
            {
                if (Id == null)
                    return 0;
                long ret;
                return long.TryParse(Id, out ret) ? ret : 0;
            }
        }

    }
}
