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

        // Запам'ятовуємо стартовий час
        long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        while (running)
        {
            sum += current;
            current += step;
            count++;

            // Перевіряємо час на кожному кроці
            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (currentTime - startTime >= 100) // Перевіряємо, чи пройшло 100 мс
            {
                startTime = currentTime; // Оновлюємо час
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