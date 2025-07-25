using UnityEngine;
using UnityEngine.UI;

public class UtilityIconColourSetter : MonoBehaviour {
    [SerializeField] private Image electricityImage;
    [SerializeField] private Image waterImage;
    [SerializeField] private GameColour electricityColour;
    [SerializeField] private GameColour waterColour;
    [SerializeField] private GameColour propertyGroupIconColour;
    [SerializeField] private GameColour mortgageColour;
    private float zeroPropertiesAlpha;
    private PlayerInfo playerInfo;


    public void setup(float zeroPropertiesAlpha, PlayerInfo playerInfo) {
        this.zeroPropertiesAlpha = zeroPropertiesAlpha;
        this.playerInfo = playerInfo;
    }
    public void setColour(UtilityType utilityType) {
        Image image = utilityType == UtilityType.ELECTRICITY ? electricityImage : waterImage;
        Color colour = propertyGroupIconColour.Colour;
        colour.a = zeroPropertiesAlpha;
        image.color = colour;
    }
    public void setColour(PlayerInfo playerInfo, UtilityInfo utilityInfo) {
        Image image = utilityInfo.UtilityType == UtilityType.ELECTRICITY ? electricityImage : waterImage;

        Color colour;
        if (utilityInfo.Owner != playerInfo) {
            colour = propertyGroupIconColour.Colour;
            colour.a = zeroPropertiesAlpha;
        }
        else if (utilityInfo.IsMortgaged) {
            colour = mortgageColour.Colour;
        }
        else {
            colour = utilityInfo.UtilityType == UtilityType.ELECTRICITY
                ? electricityColour.Colour
                : waterColour.Colour;
        }
        image.color = colour;
    }
}
