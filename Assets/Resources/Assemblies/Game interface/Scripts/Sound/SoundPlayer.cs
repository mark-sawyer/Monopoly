using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] private AudioSource[] audioSources;



    #region Singleton boilerplate
    public static SoundPlayer Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region public
    public void playSound(AudioClip sound) {
        AudioSource audioSource = getAvailableAudioSource();
        audioSource.clip = sound;
        audioSource.Play();
    }
    #endregion



    #region private
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
