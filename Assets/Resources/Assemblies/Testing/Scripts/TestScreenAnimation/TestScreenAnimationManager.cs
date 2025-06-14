using UnityEngine;

public class TestScreenAnimationManager : MonoBehaviour {
    [SerializeField] private GameEvent spinningPoliceman;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            spinningPoliceman.invoke();
        }
    }
}
