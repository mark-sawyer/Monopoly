using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableGhost : MonoBehaviour {
    [SerializeField] private Graphic[] transparentGraphics;
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



    #region protected
    protected virtual void released(bool received) { }
    #endregion



    #region private
    private void holdGhost() {
        transform.position = Input.mousePosition;
        GhostReceiver ghostReceiverUnderPointer = getGhostReceiverUnderPointer();
        bool overGhostReceiver = ghostReceiverUnderPointer != null;
        bool canBeReceived = overGhostReceiver && ghostReceiverUnderPointer.canReceiveThisGhost(this);
        if (canBeReceived && currentAlpha != 1) {
            changeTransparency(1);
        }
        else if (!canBeReceived && currentAlpha != TRANSPARENT_ALPHA) {
            changeTransparency(TRANSPARENT_ALPHA);
        }
    }
    private void releaseGhost() {
        GhostReceiver ghostReceiverUnderPointer = getGhostReceiverUnderPointer();
        bool overGhostReceiver = ghostReceiverUnderPointer != null;
        bool canBeReceived = overGhostReceiver && ghostReceiverUnderPointer.canReceiveThisGhost(this);
        if (canBeReceived) {
            ghostReceiverUnderPointer.receiveGhost(this);
        }
        released(canBeReceived);
        Destroy(gameObject);
    }
    private void changeTransparency(float alpha) {
        foreach (Graphic g in transparentGraphics) {
            Color currentColour = g.color;
            Color transparent = new Color(
                currentColour.r,
                currentColour.g,
                currentColour.b,
                alpha
            );
            g.color = transparent;
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
