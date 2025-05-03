using UnityEngine;

internal class Dice : DiceInfo {
    private Vector2Int[] lastThreeRolls = new Vector2Int[3];
    private Die[] dice = { new Die(), new Die() };



    #region DiceInfo
    public int getTotalValue() {
        return dice[0].getValue() + dice[1].getValue();
    }
    public int getDieValue(int i) {
        return dice[i].getValue();
    }
    #endregion



    #region internal
    internal void roll() {
        dice[0].roll();
        dice[1].roll();
        lastThreeRolls[2] = lastThreeRolls[1];
        lastThreeRolls[1] = lastThreeRolls[0];
        lastThreeRolls[0] = new Vector2Int(dice[0].getValue(), dice[1].getValue());
    }
    #endregion
}
