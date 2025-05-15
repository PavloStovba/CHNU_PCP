package model;

public class Producer extends Thread {
    private final Storage storage;
    private final int itemsToProduce;
    private final int id;

    public Producer(int id, Storage storage, int itemsToProduce) {
        this.id = id;
        this.storage = storage;
        this.itemsToProduce = itemsToProduce;
    }

    @Override
    public void run() {
        try {
            for (int i = 0; i < itemsToProduce; i++) {
                storage.produce(i, id);
                Thread.sleep(100);
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
