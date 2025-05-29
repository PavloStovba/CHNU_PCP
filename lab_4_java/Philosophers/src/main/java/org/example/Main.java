package org.example;
//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        while (true) {
            System.out.println("Choose solution");
            System.out.println("1 - Asymmetric solution (reversing the order of the forks)");
            System.out.println("2 - Solution with waiters (2 tokens)");
            System.out.println("3 - Hunger-checking solution");
            System.out.println("0 - Exit");
            System.out.print("Your choice: ");

            int choice = scanner.nextInt();

            switch (choice) {
                case 1:
                    runAsymmetricSolution();
                    break;
                case 2:
                    runWaiterSolution();
                    break;
                case 3:
                    runHungryCheckSolution();
                    break;
                case 0:
                    System.out.println("Close");
                    scanner.close();
                    return;
                default:
                    System.out.println("Incorrect selection. Please try again.");
            }

            scanner.nextLine();
            scanner.nextLine();
        }
    }

    private static void runAsymmetricSolution() {

        for (int run = 1; run <= 15; run++) {
            System.out.println("\n--- Launching #" + run + " ---");
            AsymmetricTable table = new AsymmetricTable();
            Thread[] philosophers = new Thread[5];

            for (int i = 0; i < 5; i++) {
                philosophers[i] = new AsymmetricPhilosopher(i, table);
            }

            for (Thread philosopher : philosophers) {
                try {
                    philosopher.join();
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    return;
                }
            }
            System.out.println("Launching #" + run + " completed successfully");

            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return;
            }
        }
    }

    private static void runWaiterSolution() {

        for (int run = 1; run <= 15; run++) {
            System.out.println("\n--- Launching #" + run + " ---");
            WaiterTable table = new WaiterTable();
            Thread[] philosophers = new Thread[5];

            for (int i = 0; i < 5; i++) {
                philosophers[i] = new WaiterPhilosopher(i, table);
            }

            for (Thread philosopher : philosophers) {
                try {
                    philosopher.join();
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    return;
                }
            }
            System.out.println("Launching #" + run + " completed successfully");

            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return;
            }
        }
    }

    private static void runHungryCheckSolution() {

        for (int run = 1; run <= 15; run++) {
            System.out.println("\n--- Launching #" + run + " ---");
            HungryCheckTable table = new HungryCheckTable();
            Thread[] philosophers = new Thread[5];

            for (int i = 0; i < 5; i++) {
                philosophers[i] = new HungryCheckPhilosopher(i, table);
            }

            for (Thread philosopher : philosophers) {
                try {
                    philosopher.join();
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    return;
                }
            }
            System.out.println("Launching #" + run + " completed successfully");

            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return;
            }
        }
    }
}