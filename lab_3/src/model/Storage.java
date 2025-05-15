package model;

import java.util.LinkedList;
import java.util.Queue;
import java.util.concurrent.Semaphore;

public class Storage {
    private final Queue<Integer> buffer = new LinkedList<>();
    private final int capacity;

    private final Semaphore emptySlots;
    private final Semaphore fullSlots;
    private final Semaphore mutex;

    public Storage(int capacity) {
        this.capacity = capacity;
        this.emptySlots = new Semaphore(capacity);
        this.fullSlots = new Semaphore(0);
        this.mutex = new Semaphore(1);
    }

    public void produce(int item, int producerId) throws InterruptedException {
        emptySlots.acquire();
        mutex.acquire();

        buffer.add(item);
        System.out.println("Producer " + producerId + " produced: " + item);

        mutex.release();
        fullSlots.release();
    }

    public int consume(int consumerId) throws InterruptedException {
        fullSlots.acquire();
        mutex.acquire();

        int item = buffer.remove();
        System.out.println("Consumer " + consumerId + " consumed: " + item);

        mutex.release();
        emptySlots.release();
        return item;
    }
}
    