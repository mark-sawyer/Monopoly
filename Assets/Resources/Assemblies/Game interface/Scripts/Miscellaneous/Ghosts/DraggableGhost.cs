using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableGhost : MonoBehaviour {
    [SerializeField] private Image[] transparentImages;
    [SerializeField] private Canvas thisCanvas;
    private float currentAlpha;
    private const float TRANSPARENT_ALPHA = 0.5f;



    #region MonoBehaviour
    private void Start() {
        changeTransparency(TRANSPARENT_ALPHA);
        thisCanvas.overrideSorting = true;
        thisCanvas.sortingOrder = 10;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        transform.position = Input.mousePosition;
    }
    private void Update() {
        if (Input.GetMouseButton(0)) holdGhost();
        else releaseGhost();
    }
    #endregion



    #region public
    public abstract void ghostSetup();
    #endregion



    #region private
    private void holdGhost() {
        transform.position = Input.mousePosition;
        bool overGhostReceiver = getGhostReceiverUnderPointer() != null;
        if (overGhostReceiver && currentAlpha != 1) {
            changeTransparency(1);
        }
        else if (!overGhostReceiver && currentAlpha != TRANSPARENT_ALPHA) {
            changeTransparency(TRANSPARENT_ALPHA);
        }
    }
    private void releaseGhost() {
        GhostReceiver ghostReceiver = getGhostReceiverUnderPointer();
        if (ghostReceiver != null) {
            ghostReceiver.receiveGhost(this);
        }
        Destroy(gameObject);
    }
    private void changeTransparency(float alpha) {
        foreach (Image image in transparentImages) {
            Color currentColour = image.color;
            Color transparent = new Color(
                currentColour.r,
                currentColour.g,
                currentColour.b,
                alpha
            );
            image.color = transparent;
        }
        currentAlpha = alpha;
    }
    private GhostReceiver getGhostReceiverUnderPointer() {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        GhostReceiver ghostReceiver = null;
        foreach (RaycastResult result in results) {
            ghostReceiver = result.gameObject.GetComponent<GhostReceiver>();
            if (ghostReceiver != null) break;
        }
        return ghostReceiver;
    }
    #endregion
}
