using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public interface IBackgroundTask
    {
        string Name { get; }

        bool IsRunning
        {
            get;
        }

        void Start();

        void Stop();
    }
}
