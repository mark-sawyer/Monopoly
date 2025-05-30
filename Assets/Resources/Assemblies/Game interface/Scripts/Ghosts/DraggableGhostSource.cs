using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableGhostSource : MonoBehaviour, IPointerDownHandler {
    [SerializeField] private GameObject ghostPrefab;
    private GameObject ghost;
    private float currentAlpha;
    private const float TRANSPARENT_ALPHA = 0.5f;



    #region MonoBehaviour
    private void Update() {
        if (ghost == null) return;
        if (Input.GetMouseButton(0)) holdGhost();
        else releaseGhost();
    }
    #endregion



    #region IPointerDownHandler
    public virtual void OnPointerDown(PointerEventData eventData) {
        ghost = Instantiate(ghostPrefab, transform);
        ghost.GetComponent<Ghostable>().ghostSetup();
        changeGhostTransparency(imagesToBecomeTransparent(ghost), TRANSPARENT_ALPHA);
        makeGhostAppearOnTop(ghost);
        ghost.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        ghost.transform.position = Input.mousePosition;
    }
    #endregion



    #region DraggableGhost
    public abstract Image[] imagesToBecomeTransparent(GameObject ghost);
    #endregion



    #region private
    private void holdGhost() {
        ghost.transform.position = Input.mousePosition;
        bool overGhostReceiver = getGhostReceiverUnderPointer() != null;
        if (overGhostReceiver && currentAlpha != 1) {
            changeGhostTransparency(imagesToBecomeTransparent(ghost), 1);
        }
        else if (!overGhostReceiver && currentAlpha != TRANSPARENT_ALPHA) {
            changeGhostTransparency(imagesToBecomeTransparent(ghost), TRANSPARENT_ALPHA);
        }
    }
    private void releaseGhost() {
        GhostReceiver ghostReceiver = getGhostReceiverUnderPointer();
        if (ghostReceiver != null) {
            ghostReceiver.receiveGhost(ghost.GetComponent<Ghostable>());
        }
        Destroy(ghost);
        ghost = null;
    }
    private void changeGhostTransparency(Image[] images, float alpha) {
        Color transparent = new Color(1f, 1f, 1f, alpha);
        foreach (Image image in images) {
            image.color = transparent;
        }
        currentAlpha = alpha;
    }
    private void makeGhostAppearOnTop(GameObject newGhost) {
        Canvas canvas = newGhost.GetComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 10;
    }
    private GhostReceiver getGhostReceiverUnderPointer() {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        GhostReceiver ghostReceiver = null;
        foreach (var result in results) {
            ghostReceiver = result.gameObject.GetComponent<GhostReceiver>();
            if (ghostReceiver != null) break;
        }
        return ghostReceiver;
    }
    #endregion
}
