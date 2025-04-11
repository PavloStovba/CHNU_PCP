using System;
using System.Threading;

class SumThread
{
    private readonly int id;
    private readonly int step;
    private bool running = true;

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

        long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        while (running)
        {
            sum += current;
            current += step;
            count++;


            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (currentTime - startTime >= 100) 
            {
                startTime = currentTime; 
            }
        }

        Console.WriteLine($"Потiк #{id} завершено. Сума = {sum}, доданків = {count}");
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