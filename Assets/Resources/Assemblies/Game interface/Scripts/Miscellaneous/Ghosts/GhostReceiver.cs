using UnityEngine;

public abstract class GhostReceiver : MonoBehaviour {
    public abstract void receiveGhost(DraggableGhost ghost);
    public virtual bool canReceiveThisGhost(DraggableGhost ghost) {
        return true;
    }
}
