using UnityEngine;

[CreateAssetMenu(fileName = "New Utility", menuName = "Utility")]
internal class Utility : Property, UtilityInfo {
    [SerializeField] private UtilityType utilityType;
    [SerializeField] private UtilityGroup utilityGroup;
}
