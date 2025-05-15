using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Write("Введіть місткість буфера: ");
        int bufferSize = int.Parse(Console.ReadLine());

        Console.Write("Введіть кількість виробників: ");
        int numProducers = int.Parse(Console.ReadLine());

        Console.Write("Введіть кількість споживачів: ");
        int numConsumers = int.Parse(Console.ReadLine());

        Console.Write("Введіть загальну кількість продукції: ");
        int totalItems = int.Parse(Console.ReadLine());

        Storage storage = new Storage(bufferSize);

        int itemsPerProducer = totalItems / numProducers;
        int itemsPerConsumer = totalItems / numConsumers;

        int producerRemainder = totalItems % numProducers;
        int consumerRemainder = totalItems % numConsumers;

        for (int i = 0; i < numProducers; i++)
        {
            int items = itemsPerProducer + (i == 0 ? producerRemainder : 0);
            new Thread(new Producer(i + 1, storage, items).Run).Start();
        }

        for (int i = 0; i < numConsumers; i++)
        {
            int items = itemsPerConsumer + (i == 0 ? consumerRemainder : 0);
            new Thread(new Consumer(i + 1, storage, items).Run).Start();
        }
    }
}
