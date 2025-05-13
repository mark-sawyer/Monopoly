using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class EstateOwnershipTag : OwnershipTag {
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image colourBand;
    private EstateInfo estateInfo;

    protected override void setupAppearance() {
        estateInfo = (EstateInfo)PropertySO;
        colourBand.color = estateInfo.EstateColour;
        textMesh.text = estateInfo.AbbreviatedName;
    }
}
