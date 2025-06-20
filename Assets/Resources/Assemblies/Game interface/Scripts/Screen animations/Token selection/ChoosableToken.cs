using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
public class ChoosableToken : DraggableGhostSource {
    [SerializeField] private UIToken uiToken;
    [SerializeField] private GameObject uiTokenPrefab;



    #region IPointerDownHandler
    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);
    }
    #endregion



    #region DraggableGhost
    public override Image[] imagesToBecomeTransparent(GameObject newGhost) {
        return new Image[2] {
            newGhost.GetComponent<Image>(),
            newGhost.transform.GetChild(0).GetComponent<Image>()
        };
    }
    #endregion
}
