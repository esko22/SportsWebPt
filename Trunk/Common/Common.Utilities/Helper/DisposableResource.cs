using System;
using System.Diagnostics;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Encapsulates IDisposable and does the disposing for you
    /// </summary>
    public abstract class DisposableResource : IDisposable
    {
        #region IDisposable Members

        [DebuggerStepThrough]
        public virtual void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~DisposableResource(){
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing) {}
    }
}