
internal interface Tradable : TradableInfo {
    public int TradableOrderID { get; }
    public void giveFromOneToTwo(Player playerOne, Player playerTwo);
}

public interface TradableInfo {
    public string Abbreviation { get; }
}
