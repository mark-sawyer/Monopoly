using UnityEngine;
using TMPro;

public class EstateVisual : SpaceVisual {
    [SerializeField] SpriteRenderer colourBand;
    [SerializeField] ScriptableObject estateSO;
    [SerializeField] TextMeshPro estateName;
    [SerializeField] TextMeshPro cost;
    EstateInfo estate;

    private void Start() {
        estate = (EstateInfo)estateSO;
        if (estate.Name != "Northumberland Avenue") estateName.text = estate.Name.ToUpper();
        cost.text = "$" + estate.Cost.ToString();
        colourBand.color = estate.EstateColour; 
    }
}
