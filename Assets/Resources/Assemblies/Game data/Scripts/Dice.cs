using UnityEngine;

internal class Dice {
    private Vector2Int[] lastThreeRolls = new Vector2Int[3];
    private Die[] dice = { new Die(), new Die() };

    public void roll() {
        dice[0].roll();
        dice[1].roll();
        lastThreeRolls[2] = lastThreeRolls[1];
        lastThreeRolls[1] = lastThreeRolls[0];
        lastThreeRolls[0] = new Vector2Int(dice[0].getValue(), dice[1].getValue());
    }
    public int getValue() {
        return dice[0].getValue() + dice[1].getValue();
    }
    public bool isDoubles() {
        return dice[0] == dice[1];
    }
    public DieValueReader getDie(int index) {
        return dice[index];
    }
}
