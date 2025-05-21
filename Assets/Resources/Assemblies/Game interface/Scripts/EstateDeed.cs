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
        colourBandImage.color = estateInfo.EstateColour;
        setTexts();
    }
    private void setTexts() {
        propertyNameText.text = estateInfo.Name.ToString().ToUpper();
        costTexts[0].text = "$" + estateInfo.getRent(0).ToString();
        costTexts[1].text = "$" + (2 * estateInfo.getRent(0)).ToString();
        costTexts[2].text = "$" + estateInfo.getRent(1).ToString();
        costTexts[3].text = "$" + estateInfo.getRent(2).ToString();
        costTexts[4].text = "$" + estateInfo.getRent(3).ToString();
        costTexts[5].text = "$" + estateInfo.getRent(4).ToString();
        costTexts[6].text = "$" + estateInfo.getRent(5).ToString();
        costTexts[7].text = "$" + estateInfo.BuildingCost.ToString() + " each";
        costTexts[8].text = "$" + estateInfo.BuildingCost.ToString() + " each";
    }
    #endregion
}
