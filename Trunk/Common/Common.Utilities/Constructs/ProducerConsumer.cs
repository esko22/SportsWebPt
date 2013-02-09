using System;
using System.Collections.Generic;
using System.Threading;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Helper for producer consumer patterns.
    /// </summary>
    public class ProducerConsumer<T> : IDisposable
    {
        #region Fields

        private Thread[] workers;
        protected Queue<T> itemQueue = new Queue<T>();
        protected object itemQueueLock = new object();

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [consumed].
        /// </summary>
        public event GenericEventHandler<T> Consumed;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ProducerConsumer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="workerCount">The worker count.</param>
        /// <param name="name">The name.</param>
        public ProducerConsumer(int workerCount, String name)
        {
            if (workerCount < 1)
            {
                throw new ArgumentOutOfRangeException("workerCount", "workerCount must be greater than 0.");
            }

            CreateWorkers(workerCount, name);
        }

        #endregion

        #region Methods

        private void CreateWorkers(int workerCount, string name)
        {
            workers = new Thread[workerCount];
            for (int i = 0; i < workerCount; i++)
            {
                workers[i] = new Thread(Worker);
                if (workerCount == 1)
                {
                    workers[i].Name = name;
                }
                else
                {
                    workers[i].Name = string.Format("{0} {1}", name, i);
                }
                workers[i].IsBackground = true;
                workers[i].Start();
            }
        }

        /// <summary>
        /// Adds the specified item to the work queue.
        /// </summary>
        /// <param name="item">The item to add to the work queue.</param>
        public void Add(T item)
        {
            Add(new T[] { item });
        }

        /// <summary>
        /// Adds the specified items to the work queue.
        /// </summary>
        /// <param name="items">The items to add to the work queue.</param>
        public void Add(T[] items)
        {
            lock (itemQueueLock)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    itemQueue.Enqueue(items[i]);
                }
                Monitor.PulseAll(itemQueueLock);
            }
        }

        /// <summary>
        /// Dequeues a work item from the queue.
        /// </summary>
        /// <remarks>
        /// The default implementation simply dequeues the next item 
        /// in the queue (FIFO).
        /// </remarks>
        /// <returns>A work item from the queue.</returns>
        protected virtual T Dequeue()
        {
            T item = itemQueue.Dequeue();

            return item;
        }

        /// <summary>
        /// Consumes the specified item.
        /// </summary>
        /// <param name="item">The item to perform work on.</param>
        protected virtual void Consume(T item)
        {
            if (Consumed != null)
            {
                Consumed(item);
            }
        }

        private void Worker()
        {
            while (true)
            {
                T item;
                lock (itemQueueLock)
                {
                    while (itemQueue.Count == 0)
                    {
                        Monitor.Wait(itemQueueLock);
                    }
                    item = Dequeue();
                }
                Consume(item);
            }
        }

        private void KillThreads()
        {
            foreach (Thread thread in workers)
            {
                thread.Abort();
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            itemQueue.Clear();
            KillThreads();
        }

        #endregion
    }
}
