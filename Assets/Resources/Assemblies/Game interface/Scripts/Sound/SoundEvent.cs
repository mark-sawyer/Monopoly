using UnityEngine;

[CreateAssetMenu(menuName = "Sound/SoundEvent")]
public class SoundEvent : ScriptableObject {
    [SerializeField] private AudioClip audioClip;

    public void play() {
        SoundPlayer.Instance.playSound(audioClip);
    }
}
