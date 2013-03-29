using System;

namespace SportsWebPt.Common.ServiceStackClient
{
    public class ApiResourceRequest<TTypeOfResource>
    {
        public string Id { get; set; }

        public TTypeOfResource Resource { get; set; }
    }
}
