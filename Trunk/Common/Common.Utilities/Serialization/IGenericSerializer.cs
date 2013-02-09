using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public interface IGenericSerializer
    {
        string Serialize(object input);

        T Deserialize<T>(string input);
    }
}
