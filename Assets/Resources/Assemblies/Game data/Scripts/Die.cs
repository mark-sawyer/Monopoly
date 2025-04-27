using UnityEngine;

internal class Die : DieValueReader {
    private int value;



    internal Die() {
        roll();
    }
    internal void roll() {
        value = Random.Range(1, 7);
    }



    public int getValue() {
        return value;
    }
}
