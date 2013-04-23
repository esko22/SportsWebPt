using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Symptom
    {
        #region Properties

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public SymptomRenderType RenderType { get; set; }

        public ICollection<AreaComponent> Components { get; set; }  

        #endregion
    }
}
