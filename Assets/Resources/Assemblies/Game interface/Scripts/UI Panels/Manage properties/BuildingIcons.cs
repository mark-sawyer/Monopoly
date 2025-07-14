using UnityEngine;

public class BuildingIcons : MonoBehaviour {
    [SerializeField] private GameObject houseIconGroupGameObject;
    [SerializeField] private GameObject hotelIconGameObject;
    [SerializeField] private GameObject mortgagedGameObject;
    [SerializeField] private HouseIcon[] houseIcons;
    private EstateInfo estateInfo;



    #region public
    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public void updateIcons() {
        if (estateInfo.HasHotel) turnHotelOn();
        else if (estateInfo.IsMortgaged) toggleMortgagedOn();
        else toggleHousesOn();
    }
    #endregion



    #region private
    private void toggleHousesOn() {
        houseIconGroupGameObject.SetActive(true);
        hotelIconGameObject.SetActive(false);
        mortgagedGameObject.SetActive(false);

        int houses = estateInfo.BuildingCount;
        for (int i = 0; i < 4; i++) {
            houseIcons[i].toggleOn(i < houses);
        }
    }
    private void turnHotelOn() {
        houseIconGroupGameObject.SetActive(false);
        hotelIconGameObject.SetActive(true);
        mortgagedGameObject.SetActive(false);
    }
    private void toggleMortgagedOn() {
        houseIconGroupGameObject.SetActive(false);
        hotelIconGameObject.SetActive(false);
        mortgagedGameObject.SetActive(true);
    }
    #endregion
}
