using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private SoundEventPair[] soundEventPairs;



    #region MonoBehaviour
    private void Start() {
        foreach (SoundEventPair soundEventPair in soundEventPairs) {
            soundEventPair.GameEvent.Listeners += () => playSound(soundEventPair.AudioClip);
        }
    }
    #endregion



    #region private
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
