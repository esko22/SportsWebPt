using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Common.ServiceStackClient
{
    public abstract class ApiListRequest
    {
        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public SortDirection SortDir { get; set; }
    }

    public enum SortDirection { Asc, Desc }
}
