using System;
using System.Threading;

class ParallelMinFinder
{
    private readonly int[] array;
    private readonly int threadCount;
    private int completedThreads = 0;
    private readonly object lockObject = new object();
    private readonly int partSize;

    public int GlobalMin { get; private set; } = int.MaxValue;
    public int GlobalMinIndex { get; private set; } = -1;
    public TimeSpan ElapsedTime { get; private set; }

    public ParallelMinFinder(int[] array, int threadCount)
    {
        this.array = array;
        this.threadCount = threadCount;
        this.partSize = array.Length / threadCount;
    }

    public void FindMinimum()
    {
        var startTime = DateTime.Now;

        for (int i = 0; i < threadCount; i++)
        {
            int start = i * partSize;
            int end = (i == threadCount - 1) ? array.Length : start + partSize;
            var task = new MinFinderTask(array, start, end, this);
            var thread = new Thread(task.Run);
            thread.Start();
        }

        lock (lockObject)
        {
            while (completedThreads < threadCount)
            {
                Monitor.Wait(lockObject);
            }
        }

        ElapsedTime = DateTime.Now - startTime;
    }

    public void ReportResult(int localMin, int localIndex)
    {
        lock (lockObject)
        {
            if (localMin < GlobalMin)
            {
                GlobalMin = localMin;
                GlobalMinIndex = localIndex;
            }

            completedThreads++;
            if (completedThreads == threadCount)
            {
                Monitor.Pulse(lockObject);
            }
        }
    }
}
