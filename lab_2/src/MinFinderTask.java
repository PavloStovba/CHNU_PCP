public class MinFinderTask implements Runnable {
    private final int[] array;
    private final int start;
    private final int end;
    private final SharedState state;
    private final int totalThreads;

    public MinFinderTask(int[] array, int start, int end, SharedState state, int totalThreads) {
        this.array = array;
        this.start = start;
        this.end = end;
        this.state = state;
        this.totalThreads = totalThreads;
    }

    @Override
    public void run() {
        int localMin = Integer.MAX_VALUE;
        int localMinIndex = -1;

        for (int i = start; i < end; i++) {
            if (array[i] < localMin) {
                localMin = array[i];
                localMinIndex = i;
            }
        }

        synchronized (state.getLock()) {
            state.updateMin(localMin, localMinIndex);
            state.incrementCompletedThreads();

            if (state.getCompletedThreads() == totalThreads) {
                state.getLock().notify();
            }
        }
    }
}
