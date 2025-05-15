using System;
using System.Collections.Generic;
using System.Threading;

public class Storage
{
    private Queue<int> buffer = new Queue<int>();
    private int capacity;

    private Semaphore emptySlots;
    private Semaphore fullSlots;
    private Semaphore mutex = new Semaphore(1, 1);

    public Storage(int capacity)
    {
        this.capacity = capacity;
        emptySlots = new Semaphore(capacity, capacity);
        fullSlots = new Semaphore(0, capacity);
    }

    public void Produce(int item, int producerId)
    {
        emptySlots.WaitOne();
        mutex.WaitOne();

        buffer.Enqueue(item);
        Console.WriteLine($"Producer {producerId} produced: {item}");

        mutex.Release();
        fullSlots.Release();
    }

    public int Consume(int consumerId)
    {
        fullSlots.WaitOne();
        mutex.WaitOne();

        int item = buffer.Dequeue();
        Console.WriteLine($"Consumer {consumerId} consumed: {item}");

        mutex.Release();
        emptySlots.Release();

        return item;
    }
}
