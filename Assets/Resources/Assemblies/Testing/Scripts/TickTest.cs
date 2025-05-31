using UnityEngine;

public class TickTest : MonoBehaviour {
    [SerializeField] private DoublesTickbox doublesTickbox;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            doublesTickbox.startAppearTick();
        }
    }
}
