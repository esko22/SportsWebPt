using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class SymptomaticComponent : AreaComponent
    {
        #region Properties

        public IEnumerable<Symptom> symptoms { get; set; } 

        #endregion
    }
}
