﻿using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{

    public class BriefExercise
    {
        #region Properties

        public int id { get; set; }

        public IEnumerable<Equipment> equipment { get; set; }

        public IEnumerable<BodyRegion> bodyRegions { get; set; }

        public String name { get; set; }

        public String pageName { get; set; }

        public String medicalName { get; set; }

        public String description { get; set; }

        public IEnumerable<string> categories { get; set; }

        public String structuresInvolved { get; set; }

        #endregion
    }

    public class Exercise : BriefExercise
    {
        #region Properties

        public IEnumerable<Video> videos { get; set; }

        public IEnumerable<BodyPart> bodyParts { get; set; }

        public String tags { get; set; }

        public int duration { get; set; }

        public String difficulty { get; set; }

        public int sets { get; set; }

        public int repititions { get; set; }

        public int perWeek { get; set; }

        public int perDay { get; set; }

        public int holdFor { get; set; }

        public string holdType { get; set; }

        #endregion
    }

    public class GridExercise
    {
        #region Properties

        public int id { get; set; }

        public String bodyRegions { get; set; }

        public String name { get; set; }

        public String pageName { get; set; }

        public string categories { get; set; }

        public bool visible { get; set; }

        public String tags { get; set; }

        #endregion
    }
}
