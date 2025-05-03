using UnityEngine;

public class TestMoveToTarget : MonoBehaviour {
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Rigidbody2D rb2D;
    private Vector3 targetPosition;
    private bool isInSpace = false;

    private void Start() {
        targetPosition = targetTransform.transform.position;
    }
    private void Update() {
        if (!isInSpace) {
            Vector3 dir = getDirectionVector().normalized;
            rb2D.velocity = Vector3.ClampMagnitude((Vector3)rb2D.velocity + dir * 5 * Time.fixedDeltaTime, 10);
        }
    }
    private Vector3 getDirectionVector() {
        return targetPosition - transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform == targetTransform) {
            print("in space");
            isInSpace = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform == targetTransform) {
            print("not in space");
            isInSpace = false;
        }
    }
}
