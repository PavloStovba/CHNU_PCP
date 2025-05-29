package org.example;

class AsymmetricPhilosopher extends Thread {
    private final AsymmetricTable table;
    private final int leftFork, rightFork;
    private final int id;

    public AsymmetricPhilosopher(int id, AsymmetricTable table) {
        this.id = id;
        this.table = table;
        
        if (id == 4) {
            leftFork = id;
            rightFork = (id + 1) % 5;
        } else {
            rightFork = id;
            leftFork = (id + 1) % 5;
        }
        start();
    }

    @Override
    public void run() {
        for (int i = 0; i < 5; i++) {
            try {
                System.out.println("Philosopher " + id + " is thinking " + (i + 1) + " times");
                Thread.sleep((int)(Math.random() * 100));

                table.getFork(rightFork);
                System.out.println("Philosopher " + id + " picked up right fork " + rightFork);

                table.getFork(leftFork);
                System.out.println("Philosopher " + id + " picked up left fork " + leftFork);

                System.out.println("Philosopher " + id + " is eating " + (i + 1) + " times");
                Thread.sleep((int)(Math.random() * 100));

                table.putFork(leftFork);
                System.out.println("Philosopher " + id + " put down left fork " + leftFork);

                table.putFork(rightFork);
                System.out.println("Philosopher " + id + " put down right fork " + rightFork);

            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                break;
            }
        }
        System.out.println("Philosopher " + id + " finished eating");
    }
}
