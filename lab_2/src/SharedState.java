public class SharedState {
    private int globalMin = Integer.MAX_VALUE;
    private int globalMinIndex = -1;
    private int completedThreads = 0;
    private final Object lock = new Object();

    public Object getLock() {
        return lock;
    }

    public void updateMin(int value, int index) {
        if (value < globalMin) {
            globalMin = value;
            globalMinIndex = index;
        }
    }

    public int getGlobalMin() {
        return globalMin;
    }

    public int getGlobalMinIndex() {
        return globalMinIndex;
    }

    public void incrementCompletedThreads() {
        completedThreads++;
    }

    public int getCompletedThreads() {
        return completedThreads;
    }
}
