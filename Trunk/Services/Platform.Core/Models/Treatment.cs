using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class Treatment
    {
        #region Properties

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public ProviderType Provider { get; set; }

        #endregion
    }
}
