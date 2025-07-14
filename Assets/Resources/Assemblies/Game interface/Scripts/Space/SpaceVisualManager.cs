using System.Collections;
using UnityEngine;

public class SpaceVisualManager : MonoBehaviour {
    #region Singleton boilerplate
    public static SpaceVisualManager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region MonoBehaviour
    private void Start() {
        ManagePropertiesEventHub.Instance.sub_UpdateBoardAfterManagePropertiesClosed(
            () => StartCoroutine(placeMortgagesHousesAndHotels())
        );
    }
    #endregion



    #region public
    public SpaceVisual getSpaceVisual(int index) {
        return transform.GetChild(index).GetComponent<SpaceVisual>();
    }
    #endregion



    #region private
    private IEnumerator placeMortgagesHousesAndHotels() {
        for (int i = 0; i < GameConstants.TOTAL_SPACES; i++) {
            SpaceInfo space = GameState.game.getSpaceInfo(i);
            if (space is not PropertySpaceInfo propertySpaceInfo) continue;
            if (propertySpaceInfo.PropertyInfo is not EstateInfo estateInfo) continue;
            EstateVisual estateVisual = (EstateVisual)getSpaceVisual(i);

            bool dataHasHotel = estateInfo.HasHotel;
            int dataBuildingCount = estateInfo.BuildingCount;
            bool displayHasHotel = estateVisual.HasHotel;
            int displayBuildingCount = estateVisual.BuildingCount;

            int[] houseHotelDifference = getHouseHotelDifference(
                dataHasHotel,
                dataBuildingCount,
                displayHasHotel,
                displayBuildingCount
            );
            if (houseHotelDifference[0] != 0 || houseHotelDifference[1] != 0) {
                yield return estateVisual.setCorrectVisuals(
                    houseHotelDifference[0],
                    houseHotelDifference[1]
                );
            }
            ManagePropertiesEventHub.Instance.call_AllVisualsUpdatedAfterManagePropertiesClosed();
        }
    }
    private int[] getHouseHotelDifference(bool dataHasHotel, int dataBuildingCount, bool displayHasHotel, int displayBuildingCount) {
        int hotelDifference;
        int houseDifference;
        if (dataHasHotel == displayHasHotel) {
            hotelDifference = 0;
            houseDifference = dataBuildingCount - displayBuildingCount;
        }
        else if (dataHasHotel) {
            hotelDifference = 1;
            houseDifference = -displayBuildingCount;
        }
        else {
            hotelDifference = -1;
            houseDifference = dataBuildingCount;
        }

        return new int[2] {
            houseDifference,
            hotelDifference
        };
    }
    #endregion
}
