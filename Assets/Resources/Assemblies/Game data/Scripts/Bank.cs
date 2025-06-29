using System.Collections.Generic;

internal class Bank : BankInfo, Creditor {
    private Queue<House> houses;
    private Queue<Hotel> hotels;



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
    #endregion



    #region BankInfo
    public int HousesRemaining => houses.Count;
    public int HotelsRemaining => hotels.Count;
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
}
