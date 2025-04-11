using System;
using System.Threading;

class SumThread
{
    private readonly int id;
    private readonly int step;
    private volatile bool running = true;

    private long sum = 0;
    private long count = 0;

    private Thread thread;

    public SumThread(int id, int step)
    {
        this.id = id;
        this.step = step;
    }

    public void Start()
    {
        thread = new Thread(Run);
        thread.Start();
    }

    private void Run()
    {
        int current = 0;

        while (running)
        {
            sum += current;
            current += step;
            count++;

            Thread.Sleep(100);
        }

        Console.WriteLine($"Потiк #{id} завершено. Сума = {sum}, доданкiв = {count}");
    }

    public void StopRunning()
    {
        running = false;
    }

    public void Join()
    {
        thread.Join();
    }
}
