using UnityEngine;

public class SpinningPolicemanSpawner : MonoBehaviour {
    [SerializeField] private GameObject spinningPolicemanPrefab;
    [SerializeField] private GameEvent spinningPolicemanEvent;

    private void Start() {
        spinningPolicemanEvent.Listeners += spawn;
    }

    private void spawn() {
        GameObject spinningPoliceman = Instantiate(spinningPolicemanPrefab, transform);
        StartCoroutine(spinningPoliceman.GetComponent<SpinningPoliceman>().STOP());
    }
}
