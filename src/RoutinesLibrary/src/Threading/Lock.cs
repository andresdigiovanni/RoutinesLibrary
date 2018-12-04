using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutinesLibrary.Threading
{
    //https://stackoverflow.com/questions/6049346/how-long-will-a-c-sharp-lock-wait-and-what-if-the-code-crashes-during-the-lock
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
                exception = ex;
            }
            finally
            {
                if (lockWasTaken)
                {
                    Monitor.Exit(oLock);
                }

                if (exception != null)
                {
                    throw new Exception("An exception occured during the lock proces.", exception);
                }
            }
        }
    }
}
