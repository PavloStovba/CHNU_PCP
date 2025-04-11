using System;
using System.Threading;

class ControllerThread
{
    private readonly SumThread[] threads;
    private readonly int[] delays;
    private Thread controller;

    public ControllerThread(SumThread[] threads, int[] delays)
    {
        this.threads = threads;
        this.delays = delays;
    }

    public void Start()
    {
        controller = new Thread(Run);
        controller.Start();
    }

    public void Join()
    {
        controller.Join();
    }

    private void Run()
    {
        long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        bool[] stopped = new bool[threads.Length];

        while (true)
        {
            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            bool allStopped = true;

            for (int i = 0; i < threads.Length; i++)
            {
                if (!stopped[i])
                {
                    allStopped = false;

                    if (currentTime - startTime >= delays[i])
                    {
                        threads[i].StopRunning();
                        stopped[i] = true;
                    }
                }
            }

            if (allStopped)
            {
                break;
            }
        }

        foreach (var t in threads)
        {
            t.Join();
        }
    }
}