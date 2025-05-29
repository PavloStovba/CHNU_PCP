using System;
using System.Threading;

public class AsymmetricTable
{
    private readonly Semaphore[] forks = new Semaphore[5];

    public AsymmetricTable()
    {
        for (int i = 0; i < forks.Length; i++)
        {
            forks[i] = new Semaphore(1, 1);
        }
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
