using UnityEngine;
using UnityEngine.UI;

public class EstateHighlight : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private GameObject mortgageSquarePanel;
    [SerializeField] private GameColour mortgageColour;
    [SerializeField] private GameColour houseColour;
    [SerializeField] private GameColour hotelColour;
    private PlayerInfo playerInfo;
    private EstateGroupInfo estateGroupInfo;
    private EstateInfo estateInfo;
    private Color highlightColour;
    private Color noColour;



    #region public
    public void setup(EstateInfo estateInfo, PlayerInfo playerInfo) {
        this.estateInfo = estateInfo;
        this.playerInfo = playerInfo;
        estateGroupInfo = estateInfo.EstateGroupInfo;
        EstateGroupColours estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateInfo.EstateColour);
        highlightColour = estateGroupColours.HighlightColour.Colour;
        noColour = new Color(0f, 0f, 0f, 0f);
    }
    public void setHighlight() {
        if (estateInfo.Owner != playerInfo)
            notOwnedByPlayer();
        else if (estateInfo.IsMortgaged)
            estateIsMortgaged();
        else if (estateInfo.HasHotel)
            estateHasHotel();
        else if (estateGroupInfo.MinBuildingCount > 0)
            groupHasBuilding();
        else if (estateInfo.BuildingCount > 0)
            groupHasBuilding();
        else
            noBuildingsInGroup();
    }
    public void turnOffHighlight() {
        notOwnedByPlayer();
    }
    #endregion



    #region private
    private void notOwnedByPlayer() {
        mortgageSquarePanel.SetActive(false);
        image.color = noColour;
    }
    private void estateIsMortgaged() {
        if (estateGroupInfo.propertiesMortgagedByPlayer(playerInfo) == estateGroupInfo.NumberOfPropertiesInGroup) {
            mortgageSquarePanel.SetActive(false);
            image.color = noColour;
        }
        else {
            mortgageSquarePanel.SetActive(true);
            image.color = mortgageColour.Colour;
        }
    }
    private void estateHasHotel() {
        mortgageSquarePanel.SetActive(false);

        if (estateGroupInfo.MaxBuildingCount == 1) image.color = noColour;
        else image.color = hotelColour.Colour;
    }
    private void groupHasBuilding() {
        mortgageSquarePanel.SetActive(false);

        if (estateGroupInfo.HotelExists) {
            image.color = noColour;
            return;
        }

        int maxBuildings = estateGroupInfo.MaxBuildingCount;
        int minBuildings = estateGroupInfo.MinBuildingCount;
        if (maxBuildings == minBuildings) image.color = noColour;
        else if (estateInfo.BuildingCount == maxBuildings) image.color = houseColour.Colour;
        else image.color = noColour;
    }
    private void noBuildingsInGroup() {
        mortgageSquarePanel.SetActive(false);

        int estatesOwnedByPlayer = estateGroupInfo.propertiesOwnedByPlayer(playerInfo);
        int estateInGroup = estateGroupInfo.NumberOfPropertiesInGroup;
        if (estatesOwnedByPlayer == estateInGroup) image.color = noColour;
        else image.color = highlightColour;
    }
    #endregion
}
