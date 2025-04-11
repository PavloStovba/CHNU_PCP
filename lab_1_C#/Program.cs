using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Write("Введiть кiлькість потокiв: ");
        int threadCount = int.Parse(Console.ReadLine());

        Random random = new Random();
        int[] steps = new int[threadCount];
        int[] delays = new int[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            steps[i] = random.Next(1, 6);      
            delays[i] = random.Next(1, 6) * 1000;     
        }

        SumThread[] workers = new SumThread[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            workers[i] = new SumThread(i + 1, steps[i]);
            workers[i].Start();
        }

        ControllerThread controller = new ControllerThread(workers, delays);
        controller.Start();
        controller.Join();

        Console.WriteLine("Усi потоки завершено.");
    }
}
