import java.util.Scanner;
import java.util.Random;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Random random = new Random();

        System.out.print("Введіть кількість потоків: ");
        int threadCount = scanner.nextInt();

        int[] steps = new int[threadCount];
        int[] delays = new int[threadCount];

        for (int i = 0; i < threadCount; i++) {
            steps[i] = random.nextInt(5) + 1;
            delays[i] = (random.nextInt(5) + 1) * 1000;
        }

        SumThread[] workers = new SumThread[threadCount];

        for (int i = 0; i < threadCount; i++) {
            workers[i] = new SumThread(i + 1, steps[i]);
            workers[i].start();
        }

        ControllerThread controller = new ControllerThread(workers, delays);
        controller.start();

        scanner.close();
    }
}
