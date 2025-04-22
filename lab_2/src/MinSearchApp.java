public class MinSearchApp {
    private final int[] array;
    private final int threadCount;
    private final SharedState state;

    public MinSearchApp(int arraySize, int threadCount) {
        this.array = new int[arraySize];
        this.threadCount = threadCount;
        this.state = new SharedState();
    }

    public void run() {
        ArrayGenerator.generate(array);
        int partSize = array.length / threadCount;

        long startTime = System.currentTimeMillis();

        for (int i = 0; i < threadCount; i++) {
            int start = i * partSize;
            int end = (i == threadCount - 1) ? array.length : start + partSize;
            new Thread(new MinFinderTask(array, start, end, state, threadCount)).start();
        }

        synchronized (state.getLock()) {
            while (state.getCompletedThreads() < threadCount) {
                try {
                    state.getLock().wait();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }

        long endTime = System.currentTimeMillis();

        System.out.println("\n\tResult");
        System.out.println("Minimum value: " + state.getGlobalMin());
        System.out.println("Index of minimum: " + state.getGlobalMinIndex());
        System.out.println("Time taken: " + (endTime - startTime) + " ms");
    }
}
