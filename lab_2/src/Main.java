import java.util.Scanner;

public class Main {
    public static int[] array;
    public static int globalMin = Integer.MAX_VALUE;
    public static int globalMinIndex = -1;
    public static final Object lock = new Object();
    public static int completedThreads = 0;

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        System.out.print("Enter the size of the array: ");
        int arraySize = scanner.nextInt();

        System.out.print("Enter the number of threads: ");
        int threadCount = scanner.nextInt();

        array = new int[arraySize];
        ArrayGenerator.generate(array);

        int partSize = arraySize / threadCount;

        long startTime = System.currentTimeMillis();

        for (int i = 0; i < threadCount; i++) {
            int start = i * partSize;
            int end = (i == threadCount - 1) ? arraySize : start + partSize;
            new Thread(new MinFinderTask(start, end, threadCount)).start();
        }

        // Чекаємо, поки всі потоки не завершать роботу
        synchronized (lock) {
            while (completedThreads < threadCount) {
                try {
                    lock.wait();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }

        long endTime = System.currentTimeMillis();

        System.out.println("\n\tResult");
        System.out.println("Minimum value: " + globalMin);
        System.out.println("Index of minimum: " + globalMinIndex);
        System.out.println("Time taken: " + (endTime - startTime) + " ms");

        scanner.close();
    }
}
