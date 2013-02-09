using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class OrderByFilter
    {
        public string OrderBy { get; set; }

        public bool IsDescending { get; set; }

        public int Order { get; set; }
    }
}
