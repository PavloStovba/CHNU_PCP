import java.util.Random;

public class ArrayGenerator {
    public static void generate(int[] array) {
        Random random = new Random();
        for (int i = 0; i < array.length; i++) {
            array[i] = random.nextInt(100000);
        }

        int negativeIndex = random.nextInt(array.length);
        array[negativeIndex] = -random.nextInt(1000);
        System.out.println("Negative number inserted at index: " + negativeIndex + " (value: " + array[negativeIndex] + ")");
    }
}
