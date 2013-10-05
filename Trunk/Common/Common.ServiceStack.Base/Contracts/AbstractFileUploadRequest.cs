using System;

namespace SportsWebPt.Common.ServiceStack
{
    public abstract class AbstractFileUploadRequest : AbstractResourceRequest
    {
        #region Properties

        public string Name { get; set; }

        public string Directory { get; set; }

        public bool MultiPart { get; set; }

        #endregion
    }
}
