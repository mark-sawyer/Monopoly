using UnityEngine;

public class Die {
    public int value { get; private set; }

    public Die() {
        roll();
    }

    public void roll() {
        value = Random.Range(1, 7);
    }
}
