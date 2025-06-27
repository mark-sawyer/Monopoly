using UnityEngine;

public abstract class PropertySection : MonoBehaviour {
    [SerializeField] private ScriptableObject propertySO;



    #region MonoBehaviour
    private void Start() {
        setup();
    }
    #endregion



    #region PropertySection
    public PropertyInfo PropertyInfo => (PropertyInfo)propertySO;
    public virtual void setup() { }
    public abstract void refreshVisual(PlayerInfo playerInfo);
    #endregion
}
