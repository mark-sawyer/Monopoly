using System.Collections.Generic;
using System.Linq;

internal class Trade : TradeInfo {
    private Player playerOne;
    private Player playerTwo;
    private List<Tradable> tradablesOne;
    private List<Tradable> tradablesTwo;
    private int moneyPassed;
    private Player moneyGivingPlayer;



    #region internal
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
    internal void performTradeExceptMoney() {
        // The money adjustment called later by the interface so the UI is immediately synced.

        foreach (Tradable tradable in tradablesOne) {
            tradable.giveFromOneToTwo(playerOne, playerTwo);
        }
        foreach (Tradable tradable in tradablesTwo) {
            tradable.giveFromOneToTwo(playerTwo, playerOne);
        }
    }
    #endregion



    #region TradeInfo
    public PlayerInfo PlayerOne => playerOne;
    public PlayerInfo PlayerTwo => playerTwo;
    public PlayerInfo MoneyGivingPlayer => moneyGivingPlayer;
    public PlayerInfo MoneyReceivingPlayer => moneyGivingPlayer == playerOne ? playerTwo : playerOne;
    public bool MoneyWasExchanged => moneyPassed > 0;
    public bool PropertyWasExchanged => tradablesOne.Any(x => x is Property) || tradablesTwo.Any(x => x is Property);
    public bool CardWasExchanged => tradablesOne.Any(x => x is Card) || tradablesTwo.Any(x => x is Card);
    public int MoneyPassed => moneyPassed;
    #endregion
}

public interface TradeInfo {
    public PlayerInfo PlayerOne { get; }
    public PlayerInfo PlayerTwo { get; }
    public PlayerInfo MoneyGivingPlayer { get; }
    public PlayerInfo MoneyReceivingPlayer { get; }
    public bool MoneyWasExchanged { get; }
    public bool PropertyWasExchanged { get; }
    public bool CardWasExchanged { get; }
    public int MoneyPassed { get; }

}
