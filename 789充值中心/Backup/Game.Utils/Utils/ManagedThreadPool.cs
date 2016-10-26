namespace Game.Utils
{
    using System;
    using System.Collections;
    using System.Threading;

    public class ManagedThreadPool
    {
        private static int _inUseThreads;
        private const int _maxWorkerThreads = 10;
        private static object _poolLock = new object();
        private static Queue _waitingCallbacks;
        private static Game.Utils.Semaphore _workerThreadNeeded;
        private static ArrayList _workerThreads;

        static ManagedThreadPool()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _waitingCallbacks = new Queue();
            _workerThreads = new ArrayList();
            _inUseThreads = 0;
            _workerThreadNeeded = new Game.Utils.Semaphore(0);
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(new ThreadStart(ManagedThreadPool.ProcessQueuedItems));
                _workerThreads.Add(thread);
                thread.Name = "ManagedPoolThread #" + i.ToString();
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private static void ProcessQueuedItems()
        {
            while (true)
            {
                _workerThreadNeeded.WaitOne();
                WaitingCallback callback = null;
                lock (_poolLock)
                {
                    if (_waitingCallbacks.Count > 0)
                    {
                        try
                        {
                            callback = (WaitingCallback) _waitingCallbacks.Dequeue();
                        }
                        catch
                        {
                        }
                    }
                }
                if (callback != null)
                {
                    try
                    {
                        Interlocked.Increment(ref _inUseThreads);
                        callback.Callback(callback.State);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        Interlocked.Decrement(ref _inUseThreads);
                    }
                }
            }
        }

        public static void QueueUserWorkItem(WaitCallback callback)
        {
            QueueUserWorkItem(callback, null);
        }

        public static void QueueUserWorkItem(WaitCallback callback, object state)
        {
            WaitingCallback callback2 = new WaitingCallback(callback, state);
            lock (_poolLock)
            {
                _waitingCallbacks.Enqueue(callback2);
            }
            _workerThreadNeeded.AddOne();
        }

        public static void Reset()
        {
            lock (_poolLock)
            {
                try
                {
                    foreach (object obj2 in _waitingCallbacks)
                    {
                        WaitingCallback callback = (WaitingCallback) obj2;
                        if (callback.State is IDisposable)
                        {
                            ((IDisposable) callback.State).Dispose();
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    foreach (Thread thread in _workerThreads)
                    {
                        if (thread != null)
                        {
                            thread.Abort("reset");
                        }
                    }
                }
                catch
                {
                }
                Initialize();
            }
        }

        public static int ActiveThreads
        {
            get
            {
                return _inUseThreads;
            }
        }

        public static int MaxThreads
        {
            get
            {
                return 10;
            }
        }

        public static int WaitingCallbacks
        {
            get
            {
                lock (_poolLock)
                {
                    return _waitingCallbacks.Count;
                }
            }
        }

        private class WaitingCallback
        {
            private WaitCallback _callback;
            private object _state;

            public WaitingCallback(WaitCallback callback, object state)
            {
                this._callback = callback;
                this._state = state;
            }

            public WaitCallback Callback
            {
                get
                {
                    return this._callback;
                }
            }

            public object State
            {
                get
                {
                    return this._state;
                }
            }
        }
    }
}

