using System;
using System.Threading;

class Program
{
    public static int[] array;
    public static int globalMin = int.MaxValue;
    public static int globalMinIndex = -1;
    public static readonly object lockObject = new object();

    public static int completedThreads = 0;
    public static int expectedThreads;

    static void Main(string[] args)
    {
        Console.Write("Enter the size of the array: ");
        int arraySize = int.Parse(Console.ReadLine());

        Console.Write("Enter the number of threads: ");
        int threadCount = int.Parse(Console.ReadLine());

        expectedThreads = threadCount;

        array = new int[arraySize];
        ArrayGenerator.Generate(array);

        int partSize = arraySize / threadCount;

        var startTime = DateTime.Now;

        for (int i = 0; i < threadCount; i++)
        {
            int start = i * partSize;
            int end = (i == threadCount - 1) ? arraySize : start + partSize;
            MinFinderTask task = new MinFinderTask(start, end, threadCount);
            Thread thread = new Thread(new ThreadStart(task.Run));
            thread.Start();
        }

        lock (lockObject)
        {
            while (completedThreads < threadCount)
            {
                Monitor.Wait(lockObject);
            }
        }

        var endTime = DateTime.Now;
        var duration = endTime - startTime;

        Console.WriteLine("\n=== Result ===");
        Console.WriteLine($"Minimum value: {globalMin}");
        Console.WriteLine($"Index of minimum: {globalMinIndex}");
        Console.WriteLine($"Time taken: {duration.TotalMilliseconds} ms");
    }
}
