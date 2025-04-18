public class MinFinderTask implements Runnable {
    private int start, end;
    private int totalThreads;

    public MinFinderTask(int start, int end, int totalThreads) {
        this.start = start;
        this.end = end;
        this.totalThreads = totalThreads;
    }

    @Override
    public void run() {
        int localMin = Integer.MAX_VALUE;
        int localMinIndex = -1;

        for (int i = start; i < end; i++) {
            if (Main.array[i] < localMin) {
                localMin = Main.array[i];
                localMinIndex = i;
            }
        }

        synchronized (Main.lock) {
            if (localMin < Main.globalMin) {
                Main.globalMin = localMin;
                Main.globalMinIndex = localMinIndex;
            }

            Main.completedThreads++;
            if (Main.completedThreads == totalThreads) {
                Main.lock.notify();  // Або notifyAll(), якщо сумніваємось
            }
        }
    }
}
