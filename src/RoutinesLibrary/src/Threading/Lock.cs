using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutinesLibrary.Threading
{
    public static class Lock
    {
        public static void CompletesIn(object oLock, int timeout, Action action)
        {
            Exception exception = null;
            bool lockWasTaken = false;
            try
            {
                Monitor.TryEnter(oLock, timeout, ref lockWasTaken);
                if (lockWasTaken)
                {
                    action();
                }
                else
                {
                    throw new Exception("Timeout exceeded, unable to lock.");
                }
            }
            catch (Exception ex)
            {
                // conserver l'exception
                exception = ex;
            }
            finally
            {
                // release le lock
                if (lockWasTaken)
                {
                    Monitor.Exit(oLock);
                }

                // relancer l'exception
                if (exception != null)
                {
                    throw new Exception("An exception occured during the lock proces.", exception);
                }
            }
        }
    }
}
