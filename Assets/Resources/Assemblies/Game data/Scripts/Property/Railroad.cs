using UnityEngine;

[CreateAssetMenu(fileName = "New railroad", menuName = "Railroad")]
internal class Railroad : Property, RailroadInfo {
    private const int rentOne = 25;
    private const int rentTwo = 50;
    private const int rentThree = 100;
    private const int rentFour = 200;



    #region RailroadInfo
    public int getRent(int railwaysOwned) {
        switch (railwaysOwned) {
            case 1: return rentOne;
            case 2: return rentTwo;
            case 3: return rentThree;
            case 4: return rentFour;
        }
        throw new System.Exception("Invalid number of railroads owned.");
    }
    #endregion
}
