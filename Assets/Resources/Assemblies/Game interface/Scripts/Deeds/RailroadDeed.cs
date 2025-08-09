using TMPro;
using UnityEngine;

public class RailroadDeed : PropertyDeed {
    [SerializeField] TextMeshProUGUI propertyNameText;



    public override void setupCard(PropertyInfo propertyInfo) {
        propertyNameText.text = propertyInfo.Name.ToUpper();
    }
}
