using System.ComponentModel;
using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    [DataContract]
    public abstract class BaseResourceRequest
    {
        [DataMember]
        [Description("The Id of the resource")]
        public string Id { get; set; }

        public long? IdAsLong
        {
            get
            {
                if (Id == null)
                {
                    return null;
                }
                long ret;
                return long.TryParse(Id, out ret) ? ret : (long?)null;
            }
        }

        public int? IdAsInt
        {
            get
            {
                if (Id == null)
                {
                    return null;
                }
                int ret;
                return int.TryParse(Id, out ret) ? ret : (int?)null;
            }
        }

        public bool IsIdCurrent
        {
            get
            {
                return !string.IsNullOrEmpty(Id) && Id.ToLower() == "current";
            }
        }
    }
}
