package org.example;

import java.util.concurrent.Semaphore;

public class AsymmetricTable {
    private final Semaphore[] forks = new Semaphore[5];

    public AsymmetricTable() {
        for (int i = 0; i < forks.length; i++) {
            forks[i] = new Semaphore(1);
        }
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
