using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameEvent rollClicked;
    [SerializeField] private AudioClip[] diceRollSounds;

    private void Start() {
        rollClicked.Listeners += playDiceSound;
    }

    private void playDiceSound() {
        int index = Random.Range(0, diceRollSounds.Length);
        audioSource.clip = diceRollSounds[index];
        audioSource.Play();
    }
}
