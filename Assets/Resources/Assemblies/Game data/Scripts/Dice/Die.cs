using UnityEngine;

internal class Die {
    private int value;



    internal Die() {
        roll();
    }
    internal void roll() {
        value = Random.Range(1, 7);
    }
    internal int getValue() {
        return value;
    }
}
