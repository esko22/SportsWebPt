﻿using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SkeletorHotspot", Namespace = "http://SportsWebPt.Platform")]
    public class SkeletorHotspotDto
    {
        #region Properties

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Region { get; set; }

        [DataMember]
        public String Orientation { get; set; }

        [DataMember]
        public String Side { get; set; }

        #endregion
    }
}