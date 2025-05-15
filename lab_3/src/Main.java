import model.*;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        System.out.print("Введіть місткість буфера: ");
        int bufferSize = scanner.nextInt();

        System.out.print("Введіть кількість виробників: ");
        int numProducers = scanner.nextInt();

        System.out.print("Введіть кількість споживачів: ");
        int numConsumers = scanner.nextInt();

        System.out.print("Введіть загальну кількість продукції: ");
        int totalItems = scanner.nextInt();

        scanner.close();

        Storage storage = new Storage(bufferSize);

        int itemsPerProducer = totalItems / numProducers;
        int itemsPerConsumer = totalItems / numConsumers;

        int remainingProducerItems = totalItems % numProducers;
        int remainingConsumerItems = totalItems % numConsumers;

        for (int i = 0; i < numProducers; i++) {
            int items = itemsPerProducer + (i == 0 ? remainingProducerItems : 0);
            new Producer(i + 1, storage, items).start();
        }

        for (int i = 0; i < numConsumers; i++) {
            int items = itemsPerConsumer + (i == 0 ? remainingConsumerItems : 0);
            new Consumer(i + 1, storage, items).start();
        }
    }
}
