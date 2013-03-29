using System.ComponentModel;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    public abstract class ApiResourceListRequest
    {
        [Description("The starting offset to read")]
        public int? Offset { get; set; }

        [Description("The maximum number of items to return")]
        public int? Limit { get; set; }

        [Description("The sort direction for the result set")]
        public SortDirection SortDir { get; set; }
    }

    public enum SortDirection { Asc, Desc }
}
