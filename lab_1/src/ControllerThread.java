public class ControllerThread extends Thread {
    private final SumThread[] threads;
    private final int[] delays;

    public ControllerThread(SumThread[] threads, int[] delays) {
        this.threads = threads;
        this.delays = delays;
    }

    @Override
    public void run() {
        Thread[] stopperThreads = new Thread[threads.length];

        for (int i = 0; i < threads.length; i++) {
            final int index = i;

            stopperThreads[i] = new Thread(() -> {
                try {
                    Thread.sleep(delays[index]);
                } catch (InterruptedException e) {
                    System.err.println("Стопер потоку #" + (index + 1) + " перерваний: " + e.getMessage());
                }
                threads[index].stopRunning();
            });

            stopperThreads[i].start();
        }

        for (SumThread thread : threads) {
            try {
                thread.join();
            } catch (InterruptedException e) {
                System.err.println("Не вдалося дочекатися завершення потоку");
            }
        }

        System.out.println("Усі потоки завершено.");
    }
}
