
internal interface Tradable : TradableInfo {
    public int TradableOrderID { get; }
}

public interface TradableInfo {
    public string Abbreviation { get; }
}
