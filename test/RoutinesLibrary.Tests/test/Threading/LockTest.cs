using RoutinesLibrary.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace RoutinesLibrary.Tests.Threading
{
    public class LockTest
    {
        private const int NUM_THREADS = 10;
        private const int DELAY_MILLISECONDS = 100;
        private const int TIMEOUT_MILLISECONDS = 1000;
        private const int INSUFICIENT_TIMEOUT_MILLISECONDS = 50;

        private List<Thread> _listThread;

        [Fact]
        public void CompletesIn_CheckTime()
        {
            DateTime start = DateTime.Now;
            CompleteInWithMultipleThreads(TIMEOUT_MILLISECONDS);
            TimeSpan totalTime = DateTime.Now - start;

            Assert.True(totalTime.Milliseconds < NUM_THREADS * TIMEOUT_MILLISECONDS);
        }

        [Fact]
        public void CompletesIn_InsuficientTime_CheckTime()
        {
            DateTime start = DateTime.Now;
            CompleteInWithMultipleThreads(INSUFICIENT_TIMEOUT_MILLISECONDS);
            TimeSpan totalTime = DateTime.Now - start;

            Assert.True(totalTime.Milliseconds < NUM_THREADS * INSUFICIENT_TIMEOUT_MILLISECONDS);
        }

        [Fact]
        public void CompletesIn_CheckConcurrency()
        {
            bool concurrentThreadsLessThanTwo = CompleteInWithMultipleThreads(TIMEOUT_MILLISECONDS);

            Assert.True(concurrentThreadsLessThanTwo);
        }

        [Fact]
        public void CompletesIn_InsuficientTime_CheckConcurrency()
        {
            bool concurrentThreadsLessThanTwo = CompleteInWithMultipleThreads(INSUFICIENT_TIMEOUT_MILLISECONDS);

            Assert.True(concurrentThreadsLessThanTwo);
        }

        private bool CompleteInWithMultipleThreads(int timeout)
        {
            int numThreadsInLockSection = 0;
            bool concurrentThreadsLessThanTwo = true;
            object lockObj = new object();
            _listThread = new List<Thread>();
            
            for (int i = 0; i < NUM_THREADS; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Lock.CompletesIn(lockObj, timeout, () =>
                    {
                        DelayFunction(ref numThreadsInLockSection, ref concurrentThreadsLessThanTwo);
                    });
                });
                thread.Start();
                _listThread.Add(thread);
            }

            WaitJoinAllThreads();
            return concurrentThreadsLessThanTwo;
        }

        private void DelayFunction(ref int numThreadsInLockSection, ref bool concurrentThreadsLessThanTwo)
        {
            IncreaseNumThreadsInLockSectionAndCheckNumber(ref numThreadsInLockSection, ref concurrentThreadsLessThanTwo);
            Thread.Sleep(DELAY_MILLISECONDS);
            DecreaseNumThreadsInLockSection(ref numThreadsInLockSection);
        }

        private void IncreaseNumThreadsInLockSectionAndCheckNumber(ref int numThreadsInLockSection, ref bool concurrentThreadsLessThanTwo)
        {
            numThreadsInLockSection++;
            concurrentThreadsLessThanTwo &= numThreadsInLockSection == 1;
        }

        private void DecreaseNumThreadsInLockSection(ref int numThreadsInLockSection)
        {
            numThreadsInLockSection--;
        }

        private void WaitJoinAllThreads()
        {
            foreach (Thread thread in _listThread)
            {
                thread.Join();
            }
        }
    }
}
