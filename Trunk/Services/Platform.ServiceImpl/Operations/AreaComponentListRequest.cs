using SportsWebPt.Common.ServiceStack.Infrastructure;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class AreaComponentListRequest : ApiResourceListRequest
    {
        #region Properties

        public int areaId { get; set; }

        #endregion
    }
}
