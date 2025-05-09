using UnityEngine;

[CreateAssetMenu(fileName = "New EstateGroup", menuName = "EstateGroup")]
internal class EstateGroup : ScriptableObject {
    public Color EstateColour { get => estateColour; }
    [SerializeField] private string groupName;
    [SerializeField] private Estate[] estates;
    [SerializeField] private Color estateColour;
}
