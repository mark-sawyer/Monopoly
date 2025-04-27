using UnityEngine;
using UnityEngine.UI;

public class DiceSound : MonoBehaviour {
    [SerializeField] private Button rollButton;
    [SerializeField] private AudioClip[] diceRollSounds;
    [SerializeField] private AudioSource audioSource;



    private void Start() {
        rollButton.onClick.AddListener(playSound);
    }
    private void playSound() {
        audioSource.clip = getRandomSound();
        audioSource.Play();
    }
    private AudioClip getRandomSound() {
        int index = Random.Range(0, diceRollSounds.Length);
        return diceRollSounds[index];
    }
}
