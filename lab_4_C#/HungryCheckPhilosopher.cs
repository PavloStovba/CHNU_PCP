using System;
using System.Threading;

public class HungryCheckPhilosopher
{
    private readonly HungryCheckTable table;
    private readonly int id;
    private readonly Thread thread;

    public HungryCheckPhilosopher(int id, HungryCheckTable table)
    {
        this.id = id;
        this.table = table;

        thread = new Thread(Run);
        thread.Start();
    }

    public void Join()
    {
        thread.Join();
    }

    private void Run()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Philosopher " + id + " is thinking " + (i + 1) + " times");
            Thread.Sleep((int)(new Random().NextDouble() * 100));

            while (!table.TryToEat(id))
            {
                Console.WriteLine("Philosopher " + id + " couldn't eat (too many hungry), waiting...");
                Thread.Sleep(50);
            }

            Console.WriteLine("Philosopher " + id + " is eating " + (i + 1) + " times");
            Thread.Sleep((int)(new Random().NextDouble() * 100));

            table.FinishEating(id);
        }
        Console.WriteLine("Philosopher " + id + " finished eating");
    }
}
