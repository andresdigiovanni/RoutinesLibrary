using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Data
{
    //https://stackoverflow.com/questions/20282111/xunit-net-how-can-i-specify-a-timeout-how-long-a-test-should-maximum-need
    public static class ActionTimeout
    {
        public static void CompletesIn(int timeout, Action action)
        {
            var task = Task.Run(action);
            var completedInTime = Task.WaitAll(new[] { task }, TimeSpan.FromMilliseconds(timeout));

            if (task.Exception != null)
            {
                if (task.Exception.InnerExceptions.Count == 1)
                {
                    throw task.Exception.InnerExceptions[0];
                }

                throw task.Exception;
            }

            if (!completedInTime)
            {
                throw new TimeoutException($"Task did not complete in {timeout} milliseconds.");
            }
        }
    }
}
