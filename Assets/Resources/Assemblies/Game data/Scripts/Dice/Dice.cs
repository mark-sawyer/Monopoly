using System.Linq;
using UnityEngine;

internal class Dice : DiceInterface {
    private Vector2Int[] lastThreeRolls = new Vector2Int[3] {
        // Initialising to non-doubles to avoid triggering ThreeDoublesInARow.
        new Vector2Int(-99, -88),
        new Vector2Int(-99, -88),
        new Vector2Int(-99, -88)
    };
    private Die[] dice = { new Die(), new Die() };



    #region DiceInfo
    public int getDieValue(int i) {
        return dice[i].getValue();
    }
    public int TotalValue => dice[0].getValue() + dice[1].getValue();
    public bool RolledDoubles => dice[0].getValue() == dice[1].getValue();
    public bool ThreeDoublesInARow => lastThreeRolls.All(x => x[0] == x[1]);
    #endregion



    #region DiceInterface
    public void roll() {
        dice[0].roll();
        dice[1].roll();
        lastThreeRolls[2] = lastThreeRolls[1];
        lastThreeRolls[1] = lastThreeRolls[0];
        lastThreeRolls[0] = new Vector2Int(dice[0].getValue(), dice[1].getValue());
    }
    public void resetDoublesCount() {
        lastThreeRolls = new Vector2Int[3] {
            new Vector2Int(-99, -88),
            new Vector2Int(-99, -88),
            new Vector2Int(-99, -88)
        };
    }
    #endregion
}
