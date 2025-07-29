using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableGhostSource : MonoBehaviour, IPointerDownHandler {
    [SerializeField] private GameObject ghostPrefab;



    #region IPointerDownHandler
    public virtual void OnPointerDown(PointerEventData eventData) {
        GameObject ghostInstance = Instantiate(ghostPrefab, transform);
        DraggableGhost draggableGhost = ghostInstance.GetComponent<DraggableGhost>();
        draggableGhost.ghostSetup();
    }
    #endregion
}
