using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    #region AudioClips
    [SerializeField] private AudioClip[] diceRollSounds;
    [SerializeField] private AudioClip questionAskedSound;
    [SerializeField] private AudioClip ching;
    #endregion
    #region GameEvents
    [SerializeField] private GameEvent rollClicked;
    [SerializeField] private GameEvent questionAsked;
    [SerializeField] private GameEvent moneyChangedHands;
    #endregion



    #region MonoBehaviour
    private void Start() {
        rollClicked.Listeners += playDiceSound;
        questionAsked.Listeners += () => playSound(questionAskedSound);
        moneyChangedHands.Listeners += () => playSound(ching);
    }
    #endregion



    #region private
    private void playDiceSound() {
        int index = Random.Range(0, diceRollSounds.Length);
        audioSource.clip = diceRollSounds[index];
        audioSource.Play();
    }
    private void playSound(AudioClip sound) {
        audioSource.clip = sound;
        audioSource.Play();
    }
    #endregion
}
