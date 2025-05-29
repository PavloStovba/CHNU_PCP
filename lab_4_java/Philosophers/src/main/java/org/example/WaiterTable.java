package org.example;

import java.util.concurrent.Semaphore;

public class WaiterTable {
    private final Semaphore[] forks = new Semaphore[5];
    private final Semaphore waiters = new Semaphore(4);

    public WaiterTable() {
        for (int i = 0; i < forks.length; i++) {
            forks[i] = new Semaphore(1);
        }
    }

    public void requestPermissionToEat() {
        try {
            waiters.acquire();
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            throw new RuntimeException(e);
        }
    }

    public void releasePermissionToEat() {
        waiters.release();
    }

    public void getFork(int id) {
        try {
            forks[id].acquire();
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            throw new RuntimeException(e);
        }
    }

    public void putFork(int id) {
        forks[id].release();
    }
}
