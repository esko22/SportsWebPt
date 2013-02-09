using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SportsWebPt.Common.Utilities
{
    public static class ReadWriterLockSlimExtensions
    {
        // Methods
        [DebuggerStepThrough]
        public static IDisposable Read(this ReaderWriterLockSlim instance)
        {
            Check.Argument.IsNotNull(instance, "instance");
            instance.EnterReadLock();
            return new DisposableCodeBlock(new Action(instance.ExitReadLock));
        }

        [DebuggerStepThrough]
        public static IDisposable ReadAndWrite(this ReaderWriterLockSlim instance)
        {
            Check.Argument.IsNotNull(instance, "instance");
            instance.EnterUpgradeableReadLock();
            return new DisposableCodeBlock(instance.ExitUpgradeableReadLock);
        }

        [DebuggerStepThrough]
        public static IDisposable Write(this ReaderWriterLockSlim instance)
        {
            Check.Argument.IsNotNull(instance, "instance");
            instance.EnterWriteLock();
            return new DisposableCodeBlock(instance.ExitWriteLock);
        }



        private sealed class DisposableCodeBlock : IDisposable
        {
            // Fields
            private readonly Action action;

            // Methods
            [DebuggerStepThrough]
            public DisposableCodeBlock(Action action)
            {
                this.action = action;
            }

            [DebuggerStepThrough]
            public void Dispose()
            {
                this.action();
            }
        }

    }
}
