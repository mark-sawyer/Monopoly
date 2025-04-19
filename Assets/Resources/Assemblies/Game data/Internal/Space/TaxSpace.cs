using UnityEngine;

[CreateAssetMenu(fileName = "New tax space", menuName = "TaxSpace")]
internal class TaxSpace : Space {
    [SerializeField] private int amount;
}
