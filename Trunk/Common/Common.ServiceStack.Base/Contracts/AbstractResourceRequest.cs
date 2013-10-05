using System.ComponentModel;
using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack
{
    [DataContract]
    public abstract class AbstractResourceRequest
    {
        [DataMember]
        [Description("The Id of the resource")]
        public string Id { get; set; }

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

        public int IdAsInt
        {
            get
            {
                if (Id == null)
                    return 0;
                int ret;
                return int.TryParse(Id, out ret) ? ret : 0;
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
