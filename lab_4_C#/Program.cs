using System;
using System.Threading;

public class PhilosophersMain
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose solution");
            Console.WriteLine("1 - Asymmetric solution (fork order change)");
            Console.WriteLine("2 - Waiter solution (2 tokens)");
            Console.WriteLine("3 - Hungry check solution");
            Console.WriteLine("0 - Exit");
            Console.Write("Your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RunAsymmetricSolution();
                    break;
                case 2:
                    RunWaiterSolution();
                    break;
                case 3:
                    RunHungryCheckSolution();
                    break;
                case 0:
                    Console.WriteLine("Close");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.ReadLine();
        }
    }

    private static void RunAsymmetricSolution()
    {

        for (int run = 1; run <= 15; run++)
        {
            Console.WriteLine("\n--- Run #" + run + " ---");
            AsymmetricTable table = new AsymmetricTable();
            AsymmetricPhilosopher[] philosophers = new AsymmetricPhilosopher[5];

            for (int i = 0; i < 5; i++)
            {
                philosophers[i] = new AsymmetricPhilosopher(i, table);
            }

            foreach (AsymmetricPhilosopher philosopher in philosophers)
            {
                philosopher.Join();
            }
            Console.WriteLine("Run #" + run + " completed successfully");

            Thread.Sleep(100);
        }
    }

    private static void RunWaiterSolution()
    {

        for (int run = 1; run <= 15; run++)
        {
            Console.WriteLine("\n--- Run #" + run + " ---");
            WaiterTable table = new WaiterTable();
            WaiterPhilosopher[] philosophers = new WaiterPhilosopher[5];

            for (int i = 0; i < 5; i++)
            {
                philosophers[i] = new WaiterPhilosopher(i, table);
            }

            foreach (WaiterPhilosopher philosopher in philosophers)
            {
                philosopher.Join();
            }
            Console.WriteLine("Run #" + run + " completed successfully");

            Thread.Sleep(100);
        }
    }

    private static void RunHungryCheckSolution()
    {

        for (int run = 1; run <= 15; run++)
        {
            Console.WriteLine("\n--- Run #" + run + " ---");
            HungryCheckTable table = new HungryCheckTable();
            HungryCheckPhilosopher[] philosophers = new HungryCheckPhilosopher[5];

            for (int i = 0; i < 5; i++)
            {
                philosophers[i] = new HungryCheckPhilosopher(i, table);
            }

            foreach (HungryCheckPhilosopher philosopher in philosophers)
            {
                philosopher.Join();
            }
            Console.WriteLine("Run #" + run + " completed successfully");

            Thread.Sleep(100);
        }
    }
}