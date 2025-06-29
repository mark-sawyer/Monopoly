using UnityEngine;

[CreateAssetMenu(fileName = "New property space", menuName = "Space/PropertySpace")]
internal class PropertySpace : Space, PropertySpaceInfo {
    [SerializeField] private Property property;



    #region internal
    internal Property Property => property;
    #endregion



    #region PropertySpaceInfo
    public PropertyInfo PropertyInfo => property;
    #endregion
}

public interface PropertySpaceInfo : SpaceInfo {
    public PropertyInfo PropertyInfo { get; }
}
