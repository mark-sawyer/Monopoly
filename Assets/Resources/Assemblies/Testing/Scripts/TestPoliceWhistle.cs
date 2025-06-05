using UnityEngine;

public class TestPoliceWhistle : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject spinningPolicemanPrefab;
    [SerializeField] private Transform canvasTransform;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject spinningPoliceman = Instantiate(spinningPolicemanPrefab, canvasTransform);
            StartCoroutine(spinningPoliceman.GetComponent<SpinningPoliceman>().STOP());
            audioSource.Play();
        }
    }
}
