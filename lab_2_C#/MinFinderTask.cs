using System;
using System.Threading;

class MinFinderTask
{
    private int start, end;
    private int totalThreads;

    public MinFinderTask(int start, int end, int totalThreads)
    {
        this.start = start;
        this.end = end;
        this.totalThreads = totalThreads;
    }

    public void Run()
    {
        int localMin = int.MaxValue;
        int localMinIndex = -1;

        for (int i = start; i < end; i++)
        {
            if (Program.array[i] < localMin)
            {
                localMin = Program.array[i];
                localMinIndex = i;
            }
        }

        lock (Program.lockObject)
        {
            if (localMin < Program.globalMin)
            {
                Program.globalMin = localMin;
                Program.globalMinIndex = localMinIndex;
            }

            Program.completedThreads++;

            if (Program.completedThreads == totalThreads)
            {
                Monitor.Pulse(Program.lockObject);
            }
        }
    }
}
