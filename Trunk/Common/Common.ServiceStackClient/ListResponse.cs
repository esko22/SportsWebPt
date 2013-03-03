using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Common.ServiceStackClient
{
    public class ListResponse<TResource, TSortField> : BaseResponse<ResourceList<TResource, TSortField>>
        where TSortField : struct
    {
        public override ResourceList<TResource, TSortField> Response { get; set; }
    }

    public class ResourceList<TResource, TSortField>
        where TSortField : struct
    {
        public TResource[] Items { get; set; }

        public int? DisplayedCount { get; set; }

        public int? TotalCount { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public SortDirection? SortDir { get; set; }

        public TSortField? SortBy { get; set; }
    }
}
