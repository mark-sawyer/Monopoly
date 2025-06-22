using UnityEngine;
using UnityEngine.UI;

public class UtilityIconColourSetter : MonoBehaviour {
    [SerializeField] private Image electricityImage;
    [SerializeField] private Image waterImage;
    [SerializeField] private GameColour electricityColour;
    [SerializeField] private GameColour waterColour;
    [SerializeField] private GameColour propertyGroupIconColour;

    public void setColour(UtilityType utilityType, bool enabled, float alpha) {
        Image image = utilityType == UtilityType.ELECTRICITY ? electricityImage : waterImage;
        Color colour = enabled ? getUtilityColour(utilityType) : propertyGroupIconColour.Colour;
        colour.a = alpha;
        image.color = colour;
    }
    private Color getUtilityColour(UtilityType utilityType) {
        return utilityType == UtilityType.ELECTRICITY ? electricityColour.Colour : waterColour.Colour;
    }
}
