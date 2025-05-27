using UnityEngine;

[CreateAssetMenu(fileName = "New tax space", menuName = "TaxSpace")]
internal class TaxSpace : Space, TaxSpaceInfo {
    [SerializeField] private TaxSpaceType taxSpaceType;
    public TaxSpaceType TaxSpaceType => taxSpaceType;
}
