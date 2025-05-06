using UnityEngine;
using TMPro;

public class EstateVisual : SpaceVisual {
    [SerializeField] SpriteRenderer colourBand;
    [SerializeField] ScriptableObject estateSO;
    [SerializeField] TextMeshPro name;
    [SerializeField] TextMeshPro cost;
    EstateInfo estate;

    private void Start() {
        estate = (EstateInfo)estateSO;
        EstateColour estateColour = estate.EstateColour;
        name.text = estate.Name.ToUpper();
        cost.text = "$" + estate.Cost.ToString();
        colourBand.color = UIUtilities.estateColourToColour(estateColour);        
    }
}
