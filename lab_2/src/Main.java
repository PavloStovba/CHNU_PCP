import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        System.out.print("Enter the size of the array: ");
        int arraySize = scanner.nextInt();

        System.out.print("Enter the number of threads: ");
        int threadCount = scanner.nextInt();

        MinSearchApp app = new MinSearchApp(arraySize, threadCount);
        app.run();

        scanner.close();
    }
}
