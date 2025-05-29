package org.example;

import java.util.concurrent.Semaphore;
import java.util.concurrent.atomic.AtomicInteger;

public class HungryCheckTable {
    private final Semaphore[] forks = new Semaphore[5];
    private final boolean[] hasLeftFork = new boolean[5];
    private final boolean[] hasRightFork = new boolean[5];
    private final AtomicInteger hungryCount = new AtomicInteger(0);
    private final Object lock = new Object();

    public HungryCheckTable() {
        for (int i = 0; i < forks.length; i++) {
            forks[i] = new Semaphore(1);
        }
    }

    public boolean tryToEat(int philosopherId) {
        synchronized (lock) {
            if (hungryCount.get() >= 4) {
                return false;
            }

            if (forks[philosopherId].tryAcquire()) {
                hasRightFork[philosopherId] = true;
                hungryCount.incrementAndGet();
                System.out.println("Philosopher " + philosopherId + " picked up right fork " + philosopherId +
                        " (hungry count: " + hungryCount.get() + ")");

                int leftFork = (philosopherId + 1) % 5;
                if (forks[leftFork].tryAcquire()) {
                    hasLeftFork[philosopherId] = true;
                    System.out.println("Philosopher " + philosopherId + " picked up left fork " + leftFork);
                    return true;
                } else {
                    forks[philosopherId].release();
                    hasRightFork[philosopherId] = false;
                    hungryCount.decrementAndGet();
                    return false;
                }
            }
            return false;
        }
    }

    public void finishEating(int philosopherId) {
        synchronized (lock) {
            int leftFork = (philosopherId + 1) % 5;

            if (hasLeftFork[philosopherId]) {
                forks[leftFork].release();
                hasLeftFork[philosopherId] = false;
                System.out.println("Philosopher " + philosopherId + " put down left fork " + leftFork);
            }

            if (hasRightFork[philosopherId]) {
                forks[philosopherId].release();
                hasRightFork[philosopherId] = false;
                hungryCount.decrementAndGet();
                System.out.println("Philosopher " + philosopherId + " put down right fork " + philosopherId +
                        " (hungry count: " + hungryCount.get() + ")");
            }
        }
    }
}
