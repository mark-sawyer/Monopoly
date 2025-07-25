using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class EstateDeed : PropertyDeed {
    [SerializeField] private Image colourBandImage;
    [SerializeField] private TextMeshProUGUI propertyNameText;
    [SerializeField] private TextMeshProUGUI[] costTexts;
    private EstateInfo estateInfo;



    #region public
    public override void setupCard(PropertyInfo propertyInfo) {
        estateInfo = (EstateInfo)propertyInfo;
        updateVisual();
    }
    #endregion



    #region private
    private void updateVisual() {
        setBandColour();
        setTexts();
    }
    private void setBandColour() {
        EstateGroupDictionary estateGroupDictionary = EstateGroupDictionary.Instance;
        EstateColour estateColour = estateInfo.EstateColour;
        EstateGroupColours estateGroupColours = estateGroupDictionary.lookupColour(estateColour);
        colourBandImage.color = estateGroupColours.EstateColour.Colour;
    }
    private void setTexts() {
        propertyNameText.text = estateInfo.Name.ToUpper();
        costTexts[0].text = "$" + estateInfo.getSpecificRent(0).ToString();
        costTexts[1].text = "$" + (2 * estateInfo.getSpecificRent(0)).ToString();
        costTexts[2].text = "$" + estateInfo.getSpecificRent(1).ToString();
        costTexts[3].text = "$" + estateInfo.getSpecificRent(2).ToString();
        costTexts[4].text = "$" + estateInfo.getSpecificRent(3).ToString();
        costTexts[5].text = "$" + estateInfo.getSpecificRent(4).ToString();
        costTexts[6].text = "$" + estateInfo.getSpecificRent(5).ToString();
        costTexts[7].text = "$" + estateInfo.BuildingCost.ToString() + " each";
        costTexts[8].text = "$" + estateInfo.BuildingCost.ToString() + " each";
    }
    #endregion
}
