using UnityEngine;

[CreateAssetMenu(fileName = "New property space", menuName = "PropertySpace")]
internal class PropertySpace : Space {
    [SerializeField] private Property property;
}
