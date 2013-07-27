using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Web.Core
{
    public class PlanExercise : Exercise
    {
        #region Properties
        
        public int sets { get; set; }

        public int repititions { get; set; }

        public int perWeek { get; set; }

        public int perDay { get; set; }

        public int refExercise { get; set; }

        public int exerciseId { get; set; }

        #endregion

    }
}
