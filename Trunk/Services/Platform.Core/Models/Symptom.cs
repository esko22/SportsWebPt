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

        public int RenderTypeId { get; set; }

        public String RenderOptions { get; set; }

        public SymptomResponseType ResponseType { get; set; }

        #region Navigation Properties
        
        public virtual SymptomRenderType RenderType { get; set; }

        public ICollection<SymptomMatrixItem> SymptomMatrixItems { get; set; }

        #endregion
        
        #endregion
    }
}
