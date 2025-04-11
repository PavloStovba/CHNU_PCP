public class SumThread extends Thread {
    private final int id;
    private final int step;
    volatile private boolean running = true;

    private long sum = 0;
    private long count = 0;

    public SumThread(int id, int step) {
        this.id = id;
        this.step = step;
    }

    @Override
    public void run() {
        int current = 0;
        while (running) {
            sum += current;
            current += step;
            count++;

//            try {
//                Thread.sleep(100);
//            } catch (InterruptedException e) {
//                return;
//            }
        }

        System.out.printf("Потік #%d завершено. Сума = %d, доданків = %d%n", id, sum, count);
    }

    // Метод для зупинки потоку
    public void stopRunning() {
        running = false;
    }
}
