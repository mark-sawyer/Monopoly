using UnityEngine;
using TMPro;

public class EstateVisual : SpaceVisual {
    [SerializeField] private SpriteRenderer colourBand;
    [SerializeField] private ScriptableObject estateSO;
    [SerializeField] private TextMeshPro estateName;
    [SerializeField] private TextMeshPro cost;

    private void Start() {
        EstateInfo estate = (EstateInfo)estateSO;
        if (estate.Name != "Northumberland Avenue") estateName.text = estate.Name.ToUpper();
        cost.text = "$" + estate.Cost.ToString();
        colourBand.color = estate.EstateColour; 
    }
}
