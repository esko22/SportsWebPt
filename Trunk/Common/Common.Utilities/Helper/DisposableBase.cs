using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public abstract class DisposableBase : IDisposable
    {
        // Fields
        private bool isDisposed;

        // Methods
        protected DisposableBase()
        {
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DebuggerStepThrough]
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.DisposeCore();
            }
            this.isDisposed = true;
        }

        [DebuggerStepThrough]
        protected virtual void DisposeCore()
        {
        }

        [DebuggerStepThrough]
        ~DisposableBase()
        {
            this.Dispose(false);
        }
    }

 

}
