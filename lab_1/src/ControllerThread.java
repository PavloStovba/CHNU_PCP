public class ControllerThread extends Thread {
    private final SumThread[] threads;
    private final int[] delays;

    public ControllerThread(SumThread[] threads, int[] delays) {
        this.threads = threads;
        this.delays = delays;
    }

    @Override
    public void run() {
        long startTime = System.currentTimeMillis();
        boolean[] stopped = new boolean[threads.length];

        while (true) {
            long currentTime = System.currentTimeMillis();
            boolean allStopped = true;

            for (int i = 0; i < threads.length; i++) {
                if (!stopped[i]) {
                    allStopped = false;
                    if (currentTime - startTime >= delays[i]) {
                        threads[i].stopRunning();
                        stopped[i] = true;
                    }
                }
            }

            if (allStopped) {
                break;
            }

//            try {
//                Thread.sleep(100);
//            } catch (InterruptedException e) {
//                System.err.println("ControllerThread interrupted: " + e.getMessage());
//            }
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
