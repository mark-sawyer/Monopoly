using TMPro;
using UnityEngine;

public class RailwayDeed : PropertyDeed {
    [SerializeField] TextMeshProUGUI propertyNameText;



    public override void setupCard(PropertyInfo propertyInfo) {
        propertyNameText.text = propertyInfo.Name.ToUpper();
    }
}
