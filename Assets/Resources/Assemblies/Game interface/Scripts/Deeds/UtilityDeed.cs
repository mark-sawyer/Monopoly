using TMPro;
using UnityEngine;

public class UtilityDeed : PropertyDeed {
    [SerializeField] TextMeshProUGUI propertyNameText;
    [SerializeField] GameObject waterWorksIcon;
    [SerializeField] GameObject electricCompanyIcon;



    public override void setupCard(PropertyInfo propertyInfo) {
        propertyNameText.text = propertyInfo.Name.ToUpper();
        UtilityInfo utilityInfo = (UtilityInfo)propertyInfo;
        UtilityType utilityType = utilityInfo.UtilityType;
        if (utilityType == UtilityType.ELECTRICITY) electricCompanyIcon.SetActive(true);
        else waterWorksIcon.SetActive(true);
    }
}
