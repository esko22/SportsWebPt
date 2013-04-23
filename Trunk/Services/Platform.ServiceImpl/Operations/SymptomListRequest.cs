using SportsWebPt.Common.ServiceStack.Infrastructure;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SymptomListRequest : ApiResourceListRequest
    {
        #region Properties

        public int componentId { get; set; }

        #endregion
    }

}
