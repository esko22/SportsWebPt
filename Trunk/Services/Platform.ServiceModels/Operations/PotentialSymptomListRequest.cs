using System;
using SportsWebPt.Common.ServiceStack.Infrastructure;

namespace SportsWebPt.Platform.ServiceModels
{
    public class PotentialSymptomListRequest : ApiResourceListRequest
    {
        #region Properties

        public int BodyPartMatrixId { get; set; }

        #endregion
    }
}
