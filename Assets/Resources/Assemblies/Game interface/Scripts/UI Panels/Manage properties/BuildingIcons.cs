using UnityEngine;

public class BuildingIcons : MonoBehaviour {
    [SerializeField] private Transform houseIconGroupTransform;
    [SerializeField] private Transform hotelIconTransform;
    [SerializeField] private HouseIcon[] houseIcons;
    private EstateInfo estateInfo;



    #region public
    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public void updateIcons() {
        if (estateInfo.HasHotel) turnHotelOn();
        else toggleHousesOn();
    }
    #endregion



    #region private
    private void turnHotelOn() {
        houseIconGroupTransform.gameObject.SetActive(false);
        hotelIconTransform.gameObject.SetActive(true);
    }
    private void toggleHousesOn() {
        hotelIconTransform.gameObject.SetActive(false);
        houseIconGroupTransform.gameObject.SetActive(true);

        int houses = estateInfo.BuildingCount;
        for (int i = 0; i < 4; i++) {
            houseIcons[i].toggleOn(i < houses);
        }
    }
    #endregion
}
