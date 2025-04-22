using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the size of the array: ");
        int arraySize = int.Parse(Console.ReadLine());

        Console.Write("Enter the number of threads: ");
        int threadCount = int.Parse(Console.ReadLine());

        int[] array = new int[arraySize];
        ArrayGenerator.Generate(array);

        var finder = new ParallelMinFinder(array, threadCount);
        finder.FindMinimum();

        Console.WriteLine("\n\tResult");
        Console.WriteLine($"Minimum value: {finder.GlobalMin}");
        Console.WriteLine($"Index of minimum: {finder.GlobalMinIndex}");
        Console.WriteLine($"Time taken: {finder.ElapsedTime.TotalMilliseconds} ms");
    }
}
