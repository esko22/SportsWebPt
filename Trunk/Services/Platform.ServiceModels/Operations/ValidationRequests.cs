using System;
using ServiceStack.ServiceHost;

namespace SportsWebPt.Platform.ServiceModels
{
    public class PageNameValidationRequest : IReturn<bool>
    {
        #region Properties

        public String PageName { get; set; }

        #endregion
    }
}
