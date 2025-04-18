using System;

class ArrayGenerator
{
    public static void Generate(int[] array)
    {
        Random random = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(100000);
        }

        int negativeIndex = random.Next(array.Length);
        array[negativeIndex] = -random.Next(1000);

        Console.WriteLine($"Negative number inserted at index: {negativeIndex} (value: {array[negativeIndex]})");
    }
}
