
internal class RiggedDice : DiceInterface, DiceValueStorer {
    private int[] storedValues = new int[2] { 1, 1 };
    private int[] diceValues = new int[2];



    #region DiceInfo
    public int getTotalValue() {
        return diceValues[0] + diceValues[1];
    }
    public int getDieValue(int i) {
        return diceValues[i];
    }
    #endregion



    #region DiceInterface
    public void roll() {
        diceValues[0] = storedValues[0];
        diceValues[1] = storedValues[1];
    }
    #endregion



    #region DiceValueStorer
    public void storeValues(int[] values) {
        storedValues[0] = values[0];
        storedValues[1] = values[1];
    }
    #endregion
}
