﻿using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class Plan
    {
        #region Properties

        public int id { get; set; }

        public IEnumerable<Exercise> exercises { get; set; }

        public IEnumerable<BodyRegion> bodyRegions { get; set; }

        public String category { get; set; }

        public String routineName { get; set; }

        public String description { get; set; }

        public String musclesInvolved { get; set; }

        public int duration { get; set; }

        public String tags { get; set; }

        public String pageName { get; set; }


        #endregion
    }
}