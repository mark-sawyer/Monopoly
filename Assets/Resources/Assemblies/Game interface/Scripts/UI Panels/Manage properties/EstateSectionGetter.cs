using UnityEngine;

public class EstateSectionGetter {
    private Transform propertySectionTransform;



    #region public
    public EstateSectionGetter(Transform propertySectionTransform) {
        this.propertySectionTransform = propertySectionTransform;
    }
    public EstateSection exe(EstateInfo estateInfo) {
        int columnIndex = getColumnIndex(estateInfo.EstateColour);
        Transform columnTransform = propertySectionTransform.GetChild(columnIndex);
        int colourIndex = (int)estateInfo.EstateColour % 2;
        Transform colourTransform = columnTransform.GetChild(colourIndex);
        int estateOrder = estateInfo.EstateOrder;
        Transform estateSectionTransform = colourTransform.GetChild(0).GetChild(estateOrder + 1);
        EstateSection estateSection = estateSectionTransform.GetComponent<EstateSection>();
        return estateSection;
    }
    #endregion



    #region private
    private int getColumnIndex(EstateColour estateColour) {
        switch (estateColour) {
            case EstateColour.BROWN: return 0;
            case EstateColour.LIGHT_BLUE: return 0;
            case EstateColour.PINK: return 1;
            case EstateColour.ORANGE: return 1;
            case EstateColour.RED: return 2;
            case EstateColour.YELLOW: return 2;
            case EstateColour.GREEN: return 3;
            case EstateColour.DARK_BLUE: return 3;
        }
        throw new System.Exception();
    }
    #endregion
}
