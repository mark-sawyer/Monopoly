using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class EstateDeed : MonoBehaviour {
    [SerializeField] private Image colourBandImage;
    [SerializeField] private ScriptableObject estate;
    [SerializeField] private TextMeshProUGUI propertyNameText;
    [SerializeField] private TextMeshProUGUI[] costTexts;
    private EstateInfo estateInfo;



    #region MonoBehaviour
    private void Start() {
        estateInfo = (EstateInfo)estate;
        updateVisual();
    }
    #endregion



    #region public
    public void updateProperty(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
        updateVisual();
    }
    #endregion



    #region private
    private void updateVisual() {
        colourBandImage.color = EstateGroupDictionary.Instance.lookupColour(estateInfo.EstateColour).EstateColour.Colour;
        setTexts();
    }
    private void setTexts() {
        propertyNameText.text = estateInfo.Name.ToString().ToUpper();
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
