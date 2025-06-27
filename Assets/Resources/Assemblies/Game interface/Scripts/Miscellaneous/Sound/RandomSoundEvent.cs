using UnityEngine;

[CreateAssetMenu(menuName = "Sound/RandomSoundEvent")]
public class RandomSoundEvent : ScriptableObject {
    [SerializeField] private AudioClip[] audioClips;

    public void play() {
        int index = Random.Range(0, audioClips.Length);
        SoundPlayer.Instance.playSound(audioClips[index]);
    }
}
