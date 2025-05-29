using System;
using System.Threading;

public class AsymmetricPhilosopher
{
    private readonly AsymmetricTable table;
    private readonly int leftFork, rightFork;
    private readonly int id;
    private readonly Thread thread;

    public AsymmetricPhilosopher(int id, AsymmetricTable table)
    {
        this.id = id;
        this.table = table;

        if (id == 4)
        {
            leftFork = id;
            rightFork = (id + 1) % 5;
        }
        else
        {
            rightFork = id;
            leftFork = (id + 1) % 5;
        }

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

            table.GetFork(rightFork);
            Console.WriteLine("Philosopher " + id + " picked up right fork " + rightFork);

            table.GetFork(leftFork);
            Console.WriteLine("Philosopher " + id + " picked up left fork " + leftFork);

            Console.WriteLine("Philosopher " + id + " is eating " + (i + 1) + " times");
            Thread.Sleep((int)(new Random().NextDouble() * 100));

            table.PutFork(leftFork);
            Console.WriteLine("Philosopher " + id + " put down left fork " + leftFork);

            table.PutFork(rightFork);
            Console.WriteLine("Philosopher " + id + " put down right fork " + rightFork);
        }
        Console.WriteLine("Philosopher " + id + " finished eating");
    }
}
