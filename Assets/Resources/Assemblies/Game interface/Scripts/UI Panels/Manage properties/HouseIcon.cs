using UnityEngine;
using UnityEngine.UI;

public class HouseIcon : MonoBehaviour {
    [SerializeField] private GameColour houseIconColour;
    [SerializeField] private GameColour faintHouseIconColour;
    [SerializeField] private Image houseImage;
    [SerializeField] private int ID;



    public void toggleHouse(bool toggle) {
        if (toggle) {
            houseImage.color = houseIconColour.Colour;
        }
        else {
            houseImage.color = faintHouseIconColour.Colour;
        }
    }
}
