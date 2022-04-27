using System;
public class Batch {
    private int _batchSize;
    public void apply(int rows, int columns, Action<int, int> function) {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                function.Invoke(i, j);
            }
        }
    }
}