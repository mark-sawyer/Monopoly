using UnityEngine;

[CreateAssetMenu(fileName = "New property space", menuName = "Space/PropertySpace")]
internal class PropertySpace : Space, PropertySpaceInfo {
    [SerializeField] private Property property;
    internal Property Property => property;



    #region PropertySpaceInfo
    public PropertyInfo PropertyInfo => property;
    #endregion
}

public interface PropertySpaceInfo : SpaceInfo {
    public PropertyInfo PropertyInfo { get; }
}
