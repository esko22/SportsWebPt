using System.Linq;
using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    [DataContract()]
    public class ResourceList<ResourceType, SortFieldType> where SortFieldType : struct
    {
        [DataMember(IsRequired = true, Order = 0, EmitDefaultValue = false)]
        public ResourceType[] Items { get; set; }

        [DataMember(IsRequired = false, Order = 1, EmitDefaultValue = false)]
        public int? DisplayedCount { get; set; }

        [DataMember(IsRequired = false, Order = 2, EmitDefaultValue = false)]
        public int? TotalCount { get; set; }

        [DataMember(IsRequired = false, Order = 3, EmitDefaultValue = false)]
        public int Offset { get; set; }

        [DataMember(IsRequired = false, Order = 4, EmitDefaultValue = false)]
        public int Limit { get; set; }

        [DataMember(IsRequired = false, Order = 5, EmitDefaultValue = false)]
        public SortDirection? SortDir { get; set; }

        [DataMember(IsRequired = false, Order = 6, EmitDefaultValue = false)]
        public SortFieldType? SortBy { get; set; }
    }

    [DataContract]
    public class ListResponse<CompactResourceType, SortFieldEnumType> : ApiResponse<ResourceList<CompactResourceType, SortFieldEnumType>> where SortFieldEnumType : struct
    {
        private ResourceList<CompactResourceType, SortFieldEnumType> resourceList;

        public ListResponse(CompactResourceType[] items, int? totalCount, int offset, int limit, SortFieldEnumType? sortBy, SortDirection? sortDir)
        {
            resourceList = new ResourceList<CompactResourceType, SortFieldEnumType>()
            {
                Items = items,
                DisplayedCount = items.Count(),
                TotalCount = totalCount,
                Offset = offset,
                Limit = limit,
                SortBy = sortBy,
                SortDir = sortDir
            };
        }

        [DataMember(IsRequired = true)]
        public override ResourceList<CompactResourceType, SortFieldEnumType> Resource { get { return resourceList; } set { resourceList = value; } }
    }
}
