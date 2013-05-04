﻿using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class SymptomaticBodyPart : BodyPart
    {
        #region Properties

        public IEnumerable<Symptom> symptoms { get; set; }

        public int bodyPartMatrixId { get; set; }

        #endregion
    }
}
