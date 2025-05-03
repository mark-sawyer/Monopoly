using UnityEngine;

public class SpaceVisual : MonoBehaviour {
    [SerializeField] private Transform spaceTarget;

    public Vector3 getTargetPosition() {
        return spaceTarget.position;
    }
}
