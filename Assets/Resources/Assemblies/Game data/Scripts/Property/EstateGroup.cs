using UnityEngine;

[CreateAssetMenu(fileName = "New EstateGroup", menuName = "EstateGroup")]
internal class EstateGroup : ScriptableObject, EstateGroupInfo {
    [SerializeField] private string groupName;
    [SerializeField] private Estate[] estates;
    [SerializeField] private Color estateColour;



    #region EstateGroupInfo
    public Color EstateColour => estateColour;
    #endregion
}
