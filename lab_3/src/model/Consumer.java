package model;

public class Consumer extends Thread {
    private final Storage storage;
    private final int itemsToConsume;
    private final int id;

    public Consumer(int id, Storage storage, int itemsToConsume) {
        this.id = id;
        this.storage = storage;
        this.itemsToConsume = itemsToConsume;
    }

    @Override
    public void run() {
        try {
            for (int i = 0; i < itemsToConsume; i++) {
                storage.consume(id);
                Thread.sleep(150);
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
