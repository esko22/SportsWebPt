using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.Web.Core
{
    public class Symptom
    {
        #region Properties

        public int id { get; set; }

        public String name { get; set; }

        public String description { get; set; }

        public String renderType { get; set; }

        #endregion
    }
}
