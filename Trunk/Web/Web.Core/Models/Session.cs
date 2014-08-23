using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class Session
    {
        #region Properties

        public Int64 id { get; set; }

        public string created { get; set; }

        public string scheduledAt { get; set; }

        public string executed { get; set; }

        public String notes { get; set; }

        public int scheduledWithId { get; set; }

        public Int64 episodeId { get; set; }

        public int differentialDiagnosisId { get; set; }

        public string sessionType { get; set; }

        #endregion
    }
}
