using System.Collections.Generic;

internal class Trade {
    private Player playerOne;
    private Player playerTwo;
    private List<Tradable> tradablesOne;
    private List<Tradable> tradablesTwo;
    private int moneyPassed;
    private Player moneyGivingPlayer;

    internal bool IsEmpty {
        get {
            return tradablesOne.Count == 0
                && tradablesTwo.Count == 0
                && moneyPassed == 0;
        }
    }
    internal Trade(Player playerOne, Player playerTwo) {
        this.playerOne = playerOne;
        this.playerTwo = playerTwo;
        tradablesOne = new();
        tradablesTwo = new();
    }
    internal void tradablesOneChange(List<Tradable> newTradables) {
        tradablesOne = newTradables;
    }
    internal void tradablesTwoChange(List<Tradable> newTradables) {
        tradablesTwo = newTradables;
    }
    internal void moneyChange(Player player, int money) {
        moneyPassed = money;
        if (money == 0) moneyGivingPlayer = null;
        else moneyGivingPlayer = player == playerOne ? playerOne : playerTwo;
    }
}
