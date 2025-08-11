using System.Collections.Generic;

internal class Bank : BankInfo, Creditor {
    private Queue<House> houses;
    private Queue<Hotel> hotels;
    private List<Tradable> eliminatedPlayerAssets;
    private Debt eliminatedPlayerDebt;



    #region internal
    internal Bank() {
        houses = initialiseHouses();
        hotels = initialiseHotels();
    }
    internal House getHouse() {
        return houses.Dequeue();
    }
    internal void returnHouse(House house) {
        houses.Enqueue(house);
    }
    internal Hotel getHotel() {
        return hotels.Dequeue();
    }
    internal void returnHotel(Hotel hotel) {
        hotels.Enqueue(hotel);
    }
    internal void takeEliminatedPlayerDebts(List<Tradable> eliminatedPlayerAssets, Debt eliminatedPlayerDebt) {
        this.eliminatedPlayerAssets = eliminatedPlayerAssets;
        this.eliminatedPlayerDebt = eliminatedPlayerDebt;
    }
    #endregion



    #region BankInfo
    public int HousesRemaining => houses.Count;
    public int HotelsRemaining => hotels.Count;
    public IEnumerable<TradableInfo> EliminatedPlayerAssets => eliminatedPlayerAssets;
    public DebtInfo EliminatedPlayerDebt => eliminatedPlayerDebt;
    #endregion



    #region private
    private Queue<House> initialiseHouses() {
        Queue<House> houseQueue = new Queue<House>(GameConstants.TOTAL_HOUSES);
        for (int i = 0; i < GameConstants.TOTAL_HOUSES; i++) {
            houseQueue.Enqueue(new House());
        }
        return houseQueue;
    }
    private Queue<Hotel> initialiseHotels() {
        Queue<Hotel> hotelQueue = new Queue<Hotel>(GameConstants.TOTAL_HOTELS);
        for (int i = 0; i < GameConstants.TOTAL_HOTELS; i++) {
            hotelQueue.Enqueue(new Hotel());
        }
        return hotelQueue;
    }
    #endregion
}

public interface BankInfo {
    public int HousesRemaining { get; }
    public int HotelsRemaining { get; }
    public IEnumerable<TradableInfo> EliminatedPlayerAssets { get; }
    public DebtInfo EliminatedPlayerDebt { get; }
}
