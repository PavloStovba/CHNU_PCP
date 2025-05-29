using System;
using System.Threading;

public class HungryCheckTable
{
    private readonly Semaphore[] forks = new Semaphore[5];
    private readonly bool[] hasLeftFork = new bool[5];
    private readonly bool[] hasRightFork = new bool[5];
    private int hungryCount = 0;
    private readonly object lockObject = new object();

    public HungryCheckTable()
    {
        for (int i = 0; i < forks.Length; i++)
        {
            forks[i] = new Semaphore(1, 1);
        }
    }

    public bool TryToEat(int philosopherId)
    {
        lock (lockObject)
        {
            if (hungryCount >= 4)
            {
                return false;
            }

            if (forks[philosopherId].WaitOne(0))
            {
                hasRightFork[philosopherId] = true;
                hungryCount++;
                Console.WriteLine("Philosopher " + philosopherId + " picked up right fork " + philosopherId +
                                 " (hungry count: " + hungryCount + ")");

                int leftFork = (philosopherId + 1) % 5;
                if (forks[leftFork].WaitOne(0))
                {
                    hasLeftFork[philosopherId] = true;
                    Console.WriteLine("Philosopher " + philosopherId + " picked up left fork " + leftFork);
                    return true;
                }
                else
                {
                    forks[philosopherId].Release();
                    hasRightFork[philosopherId] = false;
                    hungryCount--;
                    return false;
                }
            }
            return false;
        }
    }

    public void FinishEating(int philosopherId)
    {
        lock (lockObject)
        {
            int leftFork = (philosopherId + 1) % 5;

            if (hasLeftFork[philosopherId])
            {
                forks[leftFork].Release();
                hasLeftFork[philosopherId] = false;
                Console.WriteLine("Philosopher " + philosopherId + " put down left fork " + leftFork);
            }

            if (hasRightFork[philosopherId])
            {
                forks[philosopherId].Release();
                hasRightFork[philosopherId] = false;
                hungryCount--;
                Console.WriteLine("Philosopher " + philosopherId + " put down right fork " + philosopherId +
                                 " (hungry count: " + hungryCount + ")");
            }
        }
    }
}
