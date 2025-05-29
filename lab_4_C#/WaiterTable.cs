using System;
using System.Threading;

public class WaiterTable
{
    private readonly Semaphore[] forks = new Semaphore[5];
    private readonly Semaphore waiters = new Semaphore(4, 4);

    public WaiterTable()
    {
        for (int i = 0; i < forks.Length; i++)
        {
            forks[i] = new Semaphore(1, 1);
        }
    }

    public void RequestPermissionToEat()
    {
        waiters.WaitOne();
    }

    public void ReleasePermissionToEat()
    {
        waiters.Release();
    }

    public void GetFork(int id)
    {
        forks[id].WaitOne();
    }

    public void PutFork(int id)
    {
        forks[id].Release();
    }
}

