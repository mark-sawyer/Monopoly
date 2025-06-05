using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] private AudioSource[] audioSources;
    #region AudioClips
    [SerializeField] private AudioClip[] diceRollSounds;
    [SerializeField] private AudioClip questionAskedSound;
    [SerializeField] private AudioClip ching;
    [SerializeField] private AudioClip chk;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;
    [SerializeField] private AudioClip whistle;
    #endregion
    #region GameEvents
    [SerializeField] private GameEvent rollClicked;
    [SerializeField] private GameEvent questionAsked;
    [SerializeField] private GameEvent moneyChangedHands;
    [SerializeField] private GameEvent buttonPressed;
    [SerializeField] private GameEvent correct;
    [SerializeField] private GameEvent incorrect;
    [SerializeField] private GameEvent spinningPoliceman;
    #endregion



    #region MonoBehaviour
    private void Start() {
        rollClicked.Listeners += playDiceSound;
        questionAsked.Listeners += () => playSound(questionAskedSound);
        moneyChangedHands.Listeners += () => playSound(ching);
        buttonPressed.Listeners += () => playSound(chk);
        correct.Listeners += () => playSound(correctSound);
        incorrect.Listeners += () => playSound(incorrectSound);
        spinningPoliceman.Listeners += () => playSound(whistle);
    }
    #endregion



    #region private
    private void playDiceSound() {
        int index = Random.Range(0, diceRollSounds.Length);
        AudioSource audioSource = getAvailableAudioSource();
        audioSource.clip = diceRollSounds[index];
        audioSource.Play();
    }
    private void playSound(AudioClip sound) {
        AudioSource audioSource = getAvailableAudioSource();
        audioSource.clip = sound;
        audioSource.Play();
    }
    private AudioSource getAvailableAudioSource() {
        AudioSource defaultAudioSource = audioSources[0];
        for (int i = 0; i < audioSources.Length; i++) {
            AudioSource audioSource = audioSources[i];
            if (!audioSource.isPlaying) return audioSource;
        }
        return defaultAudioSource;
    }
    #endregion
}
