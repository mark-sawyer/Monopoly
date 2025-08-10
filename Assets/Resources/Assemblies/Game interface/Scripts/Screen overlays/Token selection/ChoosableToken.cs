using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
public class ChoosableToken : DraggableGhostSource {
    [SerializeField] private UIToken uiToken;
    [SerializeField] private GameObject uiTokenPrefab;



    #region DraggableGhost
    public Image[] imagesToBecomeTransparent(GameObject newGhost) {
        return new Image[2] {
            newGhost.GetComponent<Image>(),
            newGhost.transform.GetChild(0).GetComponent<Image>()
        };
    }
    #endregion
}
