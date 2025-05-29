package org.example;

public class HungryCheckPhilosopher extends Thread {
    private final HungryCheckTable table;
    private final int id;

    public HungryCheckPhilosopher(int id, HungryCheckTable table) {
        this.id = id;
        this.table = table;
        start();
    }

    @Override
    public void run() {
        for (int i = 0; i < 5; i++) {
            try {
                System.out.println("Philosopher " + id + " is thinking " + (i + 1) + " times");
                Thread.sleep((int)(Math.random() * 100));

                while (!table.tryToEat(id)) {
                    System.out.println("Philosopher " + id + " couldn't eat (too many hungry), waiting...");
                    Thread.sleep(50);
                }

                System.out.println("Philosopher " + id + " is eating " + (i + 1) + " times");
                Thread.sleep((int)(Math.random() * 100));

                table.finishEating(id);

            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                break;
            }
        }
        System.out.println("Philosopher " + id + " finished eating");
    }
}
