using System.Linq;
using UnityEngine;

internal class RiggedDice : DiceInterface, DiceValueStorer {
    private int[] storedValues = new int[2] { 1, 0 };
    private int[] diceValues = new int[2];
    private Vector2Int[] lastThreeRolls = new Vector2Int[3] {
        new Vector2Int(-99, -88),
        new Vector2Int(-99, -88),
        new Vector2Int(-99, -88)
    };



    #region DiceInfo
    public int getDieValue(int i) {
        return diceValues[i];
    }
    public int[] getNonTurnDiceRoll() {
        return new int[2] {
            Random.Range(1, 7),
            Random.Range(1, 7)
        };
    }
    public int TotalValue => diceValues[0] + diceValues[1];
    public bool RolledDoubles => lastThreeRolls[0].x == lastThreeRolls[0].y;
    public int DoublesInARow => lastThreeRolls.Count(x => x[0] == x[1]);
    #endregion



    #region DiceInterface
    public void roll() {
        diceValues[0] = storedValues[0];
        diceValues[1] = storedValues[1];
        lastThreeRolls[2] = lastThreeRolls[1];
        lastThreeRolls[1] = lastThreeRolls[0];
        lastThreeRolls[0] = new Vector2Int(diceValues[0], diceValues[1]);
    }
    public void resetDoublesCount() {
        lastThreeRolls = new Vector2Int[3] {
            new Vector2Int(-99, -88),
            new Vector2Int(-99, -88),
            new Vector2Int(-99, -88)
        };
    }
    #endregion



    #region DiceValueStorer
    public void storeValues(int[] values) {
        storedValues[0] = values[0];
        storedValues[1] = values[1];
    }
    #endregion
}
