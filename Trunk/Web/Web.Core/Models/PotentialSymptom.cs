﻿using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class PotentialSymptom : Symptom
    {
        #region Properties

        public int symptomId { get; set; }

        public String[] givenResponse { get; set; }

        public String bodyPart { get; set; }

        #endregion
    }
}
