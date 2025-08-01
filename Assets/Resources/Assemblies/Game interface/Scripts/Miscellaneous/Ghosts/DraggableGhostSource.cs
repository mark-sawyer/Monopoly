using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableGhostSource : MonoBehaviour, IPointerDownHandler {
    [SerializeField] private GameObject ghostPrefab;



    #region protected
    protected virtual void reactToCreatingGhost() { }
    #endregion



    #region IPointerDownHandler
    public virtual void OnPointerDown(PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        GameObject ghostInstance = Instantiate(ghostPrefab, transform);
        DraggableGhost draggableGhost = ghostInstance.GetComponent<DraggableGhost>();
        draggableGhost.ghostSetup();
        reactToCreatingGhost();
    }
    #endregion
}
