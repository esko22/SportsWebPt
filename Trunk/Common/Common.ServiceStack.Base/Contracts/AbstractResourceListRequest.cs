using System.ComponentModel;
using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack
{
    [DataContract]
    public abstract class AbstractResourceListRequest : AbstractResourceRequest
    {
        [DataMember]
        [Description("The starting offset to read")]
        public int Offset { get; set; }

        [DataMember]
        [Description("The maximum number of items to return")]
        public int Limit { get; set; }

        [DataMember]
        [Description("The sort direction for the result set")]
        public SortDirection SortDir { get; set; }
    }

    public enum SortDirection { Asc, Desc }

    public struct BasicSortBy {}
}
