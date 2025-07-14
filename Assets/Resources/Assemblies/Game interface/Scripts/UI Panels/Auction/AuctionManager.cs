using UnityEngine;

public class AuctionManager : MonoBehaviour {
    #region Singleton boilerplate
    public static AuctionManager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion
}
