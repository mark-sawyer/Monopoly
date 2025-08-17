using UnityEngine;

public class PropertyShower : MonoBehaviour {
    #region MonoBehaviour
    private void Start() {
        SpaceVisual spaceVisual = GetComponent<SpaceVisual>();
        SpaceInfo spaceInfo = spaceVisual.SpaceInfo;
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)spaceInfo;
        PropertyInfo = propertySpaceInfo.PropertyInfo;
    }
    #endregion



    #region public
    public PropertyInfo PropertyInfo { get; private set; }
    #endregion
}
