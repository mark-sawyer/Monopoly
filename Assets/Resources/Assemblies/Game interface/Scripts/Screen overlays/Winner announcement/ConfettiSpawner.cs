using UnityEngine;

public class ConfettiSpawner : MonoBehaviour {
    [SerializeField] private GameObject confettiPrefab;
    private int spawnTicker;
    [SerializeField] private int frames;



    private void Update() {
        spawnTicker = (spawnTicker + 1) % frames;
        if (spawnTicker == 0) {
            Instantiate(confettiPrefab, transform);
        }
    }
}
