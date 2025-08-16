using System;
using UnityEngine;

public abstract class MPPropertySection : MonoBehaviour {
    [SerializeField] private ScriptableObject propertySO;



    #region MonoBehaviour
    private void Start() {
        setup();
    }
    #endregion



    #region PropertySection
    public PropertyInfo PropertyInfo => (PropertyInfo)propertySO;
    public virtual void setup() { }
    public abstract void refreshRegularVisual(PlayerInfo playerInfo);
    public abstract void refreshBuildingPlacementVisual(PlayerInfo playerInfo);
    public Action<PlayerInfo> getCorrectRefreshFunction(bool isRegular) {
        if (isRegular) return refreshRegularVisual;
        else return refreshBuildingPlacementVisual;
    }
    #endregion
}
