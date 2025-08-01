using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;

public class EstateVisual : SpaceVisual {
    [SerializeField] private ScriptableObject estateSO;
    #region Internal references
    [SerializeField] private SpriteRenderer colourBand;
    [SerializeField] private TextMeshPro estateName;
    [SerializeField] private TextMeshPro cost;
    [SerializeField] private Transform housesParent;
    [SerializeField] private BoardBuilding[] houseBuildings;
    [SerializeField] private BoardBuilding hotelBuilding;
    #endregion
    private int numberOfHouses;
    private bool hasHotel;



    #region MonoBehaviour
    private void Start() {
        EstateInfo estate = (EstateInfo)estateSO;
        if (estate.Name != "Northumberland Avenue") estateName.text = estate.Name.ToUpper();
        cost.text = "$" + estate.Cost.ToString();
        colourBand.color = EstateGroupDictionary.Instance.lookupColour(estate.EstateColour).MainColour.Colour;
        numberOfHouses = 0;
        hasHotel = false;
    }
    #endregion



    #region public
    public int BuildingCount {
        get {
            if (HasHotel) return 1;
            else return numberOfHouses;
        }        
    }
    public bool HasHotel => hasHotel;
    public IEnumerator setCorrectVisuals(int houseDifference, int hotelDifference) {
        int framesBetween = 10;
        IEnumerator noHotelsInvolved() {
            if (houseDifference > 0) {
                for (int i = 0; i < houseDifference; i++) {
                    toggleHouse(true);
                    for (int j = 0; j < framesBetween; j++) yield return null;
                }
            }
            else {
                for (int i = 0; i < -houseDifference; i++) {
                    toggleHouse(false);
                    for (int j = 0; j < framesBetween; j++) yield return null;
                }
            }
            for (int i = 0; i < BoardBuilding.FRAMES - framesBetween; i++) yield return null;
        }
        IEnumerator addingAHotel() {
            if (houseDifference < 0) {
                for (int i = 0; i < -houseDifference; i++) {
                    toggleHouse(false);
                    for (int j = 0; j < framesBetween; j++) yield return null;
                }
                for (int i = 0; i < BoardBuilding.FRAMES - framesBetween; i++) yield return null;
            }
            toggleHotel(true);
            for (int i = 0; i < BoardBuilding.FRAMES; i++) yield return null;
        }
        IEnumerator removingAHotel() {
            toggleHotel(false);
            for (int i = 0; i < BoardBuilding.FRAMES; i++) yield return null;
            if (houseDifference > 0) {
                for (int i = 0; i < houseDifference; i++) {
                    toggleHouse(true);
                    for (int j = 0; j < framesBetween; j++) yield return null;
                }
                for (int i = 0; i < BoardBuilding.FRAMES - framesBetween; i++) yield return null;
            }
        }

        if (hotelDifference == 0) yield return noHotelsInvolved();
        else if (hotelDifference == 1) yield return addingAHotel();
        else if (hotelDifference == -1) yield return removingAHotel();
    }
    #endregion



    #region private 
    private void toggleHouse(bool toggle) {
        numberOfHouses = toggle ? numberOfHouses + 1 : numberOfHouses - 1;
        int index = toggle ? numberOfHouses - 1 : numberOfHouses;
        BoardBuilding houseBuilding = houseBuildings[index];
        if (toggle) houseBuilding.appear();
        else houseBuilding.disappear();
    }
    private void toggleHotel(bool toggle) {
        hasHotel = toggle;
        if (toggle) hotelBuilding.appear();
        else hotelBuilding.disappear();
    }
    #endregion
}
