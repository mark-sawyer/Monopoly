using UnityEngine;

public class SpaceVisualManager : MonoBehaviour {
    #region Singleton boilerplate
    public static SpaceVisualManager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region public
    public SpaceVisual getSpaceVisual(int index) {
        return transform.GetChild(index).GetComponent<SpaceVisual>();
    }
    #endregion
}
