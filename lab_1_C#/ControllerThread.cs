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
        Thread[] stoppers = new Thread[threads.Length];

        for (int i = 0; i < threads.Length; i++)
        {
            int index = i;

            stoppers[i] = new Thread(() =>
            {
                try
                {
                    Thread.Sleep(delays[index]);
                    threads[index].StopRunning();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Стопер потоку #{index + 1} перерваний: {ex.Message}");
                }
            });

            stoppers[i].Start();
        }

        foreach (var t in threads)
        {
            t.Join();
        }
    }
}
