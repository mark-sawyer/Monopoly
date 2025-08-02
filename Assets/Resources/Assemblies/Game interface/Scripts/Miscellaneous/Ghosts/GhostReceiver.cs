using UnityEngine;

public abstract class GhostReceiver : MonoBehaviour {
    public abstract void receiveGhost(DraggableGhost ghost);
    public abstract bool canReceiveThisGhost(DraggableGhost ghost);
}
